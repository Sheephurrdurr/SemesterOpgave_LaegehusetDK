using ConsoleUI;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Interfaces;
using UseCases.BookConsultation;
using UseCases.CancelConsultation;
using UseCases.ChangeConsultationType;
using UseCases.CompleteConsultation;
using UseCases.MarkArrived;
using Facade.DTOs;


// Configure appconfig connectionstring, so that we can use it to connect to the database.
// This is done by reading the appsettings.json file, which is located in the root of the project.
// The connection string is then retrieved from the configuration and used to configure the DbContext.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

// IoC enables us to inject dependencies into our classes, rather than having them create their own dependencies.
// This makes our code more testable and maintainable, as we can easily swap out implementations of our dependencies without having to change the code that uses them.
// In this case, we are using the built-in dependency injection container provided by Microsoft.Extensions.DependencyInjection to register our DbContext, repositories, use cases, and UnitOfWork.
var services = new ServiceCollection();

// Register DBContext 
services.AddDbContext<DoctorsOfficeContext>(options =>
    options.UseSqlServer(connectionString));
      

// Register repositories
services.AddScoped<IConsultationRepository, ConsultationRepository>();
services.AddScoped<IPatientRepository, PatientRepository>();
services.AddScoped<IDoctorRepository, DoctorRepository>();
services.AddScoped<IConsultationTypeRepository, ConsultationTypeRepository>();

// Register use cases
services.AddScoped<BookConsultationUseCase>();
services.AddScoped<CancelConsultationUseCase>();
services.AddScoped<ChangeConsultationTypeUseCase>();
services.AddScoped<CompleteConsultationUseCase>();
services.AddScoped<MarkArrivedUseCase>();

// Register UnitOfWork, which is responsible for saving changes to the database.
// This is used in the use cases to ensure that all changes are saved in a single transaction.
services.AddScoped<IUnitOfWork, UnitOfWork>();

var serviceProvider = services.BuildServiceProvider();

var context = serviceProvider.GetRequiredService<DoctorsOfficeContext>();

Console.WriteLine("Creating database...");
//context.Database.EnsureDeleted(); RE ENABLE THIS WHEN IM DONE WITH SETTING UP UI
context.Database.EnsureCreated();
Console.WriteLine("Database created succesfully!");

Console.WriteLine("Seeding database...");
if(!context.Doctors.Any() || !context.Patients.Any() || !context.ConsultationTypes.Any() || !context.Consultations.Any())
{
    var seeder = new TestDataGenerator(context);
    seeder.GenerateConsultationTypes();
    seeder.GenerateDoctors(100);
    seeder.GeneratePatients(500);
    seeder.GenerateConsultations(10000);
}
Console.WriteLine("Test data generation completed!");

Console.WriteLine("Database has been seeded.");

// Test use case by booking a consultation with the first doctor, patient and consultation type in the database.
var bookingUseCase = serviceProvider.GetRequiredService<BookConsultationUseCase>();

var doctor = context.Doctors.First();
var patient = context.Patients.First();
var consultationType = context.ConsultationTypes.First();

var request = new BookConsultationRequest
{
    DoctorId = doctor.Id,
    PatientId = patient.Id,
    ConsultationTypeId = consultationType.Id,
    StartTime = DateTime.Now.AddDays(1)
};

var response = await bookingUseCase.ExecuteAsync(request);
Console.WriteLine(response.Message);

Console.WriteLine("Performance testing...");
var performanceTester = new PerformanceTester(context);
performanceTester.TestLazyLoadingNPlus1();
performanceTester.TestEagerLoading();
performanceTester.TestExplicitLoading(); 
performanceTester.TestRawSql();

Console.WriteLine("Bit of complex queries...");

                                                    // Prepare for a comment novel \\

// Query finds all consultations that overlap with another consultation for the same doctor.
// First step is to join the Consultations with ConsultationTypes to get the duration of each consultation,
// because we need to know when each consultation ends in order to check for overlaps.

var consultationsWithDuration = context.Consultations // Get all the consultations
    .Join(context.ConsultationTypes, // Join consultations onto consultation types, so we can get the duration of each consultation.
    c => c.ConsultationTypeId, // joins the ConsultationTypeId from the Consultation to the Id of the ConsultationType
    ct => ct.Id, // and the other way around
    (c, ct) => new { Consultation = c, Duration = ct.Duration }) // Define an anonymous object that holds both the consultation and its duration, so we can use them in the next step of the query.
    .ToList(); // throw them into a list, so we can use them for the actual query we're making

// Now that we have the consultations and their durations locked and loaded in a list,
// we can check for overlaps by comparing each consultation to every other consultation for the same doctor.
var overlapping = consultationsWithDuration 
    .Where(a1 => consultationsWithDuration 
        .Any(a2 => 
            a2.Consultation.Id != a1.Consultation.Id && // make sure we're not comparing the same consultation to itself
            a2.Consultation.DoctorId == a1.Consultation.DoctorId && // make sure we're only comparing consultations for the same doctor
            a2.Consultation.TimeSlot.StartTime < a1.Consultation.TimeSlot.StartTime.Add(a1.Duration + TimeSpan.FromMinutes(5)) && // check if the start time of a2 is before the end time of a1
                                                                                                                                  // (which is the start time of a1 plus its duration, plus a 5 minute buffer)

            a2.Consultation.TimeSlot.StartTime.Add(a2.Duration + TimeSpan.FromMinutes(5)) > a1.Consultation.TimeSlot.StartTime)) // do the same but the other way around, check if the end time of a2 is after the start time of a1
    .ToList(); // Ladies and gentlemen, we got him.

Console.WriteLine($"Found {overlapping.Count} overlapping consultations."); 


// Query finds all available time slots for a doctor on a specific day, based on their existing consultations.
// This query takes two parameters. DoctorId and Date.
var doctorId = context.Doctors.First().Id;
var date = context.Consultations
    .Where(c => c.DoctorId == doctorId)
    .First().TimeSlot.StartTime;


var AvailableTimesForDoctor = context.Consultations
    .Where(c => c.TimeSlot.StartTime.Date == date.Date && c.DoctorId == doctorId) // Get all consultations for the doctor on the specified date
    .OrderBy(c => c.TimeSlot.StartTime) // Order them by start time, so we can check for gaps between consultations in the next step.
    .ToList();

// For our next trick, find available timeslots for doctors!
// For this we shall use some volunteers.
// How about workDayStart and workDayEnd? They will represent the start and end of the doctor's work day. Fuck, what is happening to me
// And also we need to keep track of the time- we do that with currentTime
// Besides, of course, an object that we can hold those times in. Lets get it on.
var workDayStart = date.Date.AddHours(8);
var workDayEnd = date.Date.AddHours(16);
var currentTime = workDayStart;
var availableTimeSlots = new List<(DateTime From ,DateTime To)>();

// Loop through the consultations in the AvailabelTimesforDoctor query
foreach (var consultation in AvailableTimesForDoctor)
{
    if (currentTime < consultation.TimeSlot.StartTime) // If the current time is less (so before) the start time of the consultation
    {
        availableTimeSlots.Add((currentTime, consultation.TimeSlot.StartTime)); // we add that one to the list of available time slots
    }
    currentTime = consultation.TimeSlot.EndTime; // and reset the current time, so it stays on track
}

if (currentTime < workDayEnd) // If the current time before the end of the work day
{
    availableTimeSlots.Add((currentTime, workDayEnd)); // we add the remaining time to the list of available time slots
}

foreach(var slot in availableTimeSlots)
{
    Console.WriteLine($"Available slot: {slot.From:HH:mm} - {slot.To:HH:mm}");
}

    // Query finds "todays overview"
    var todaysOverview = context.Consultations
        .Where(c => c.TimeSlot.StartTime.Date == DateTime.Now.AddDays(1).Date
        && c.DoctorId == doctorId) // Get all consultations for today
        .Join(context.ConsultationTypes,
            c => c.ConsultationTypeId,
            cType => cType.Id,
            (c, ct) => new { Consultation = c, ConsultationType = ct })

        .Join(context.Doctors,
            x => x.Consultation.DoctorId,
            d => d.Id,
            (x, d) => new { x.Consultation, x.ConsultationType, Doctor = d })

        .Join(context.Patients,
            x => x.Consultation.PatientId,
            p => p.Id,
            (x, p) => new { x.Consultation, x.ConsultationType, x.Doctor, Patient = p })
        .OrderBy(x => x.Consultation.TimeSlot.StartTime)
        .ToList();

    Console.WriteLine("Today's Overview:");
    foreach(var item in todaysOverview)
    {
        Console.WriteLine($"{item.Consultation.TimeSlot.StartTime:HH:mm} - {item.Consultation.TimeSlot.EndTime:HH:mm}");
        Console.WriteLine($"{item.ConsultationType.Name} med {item.Patient.Name}. Læge: {item.Doctor.Name}\n");
    }

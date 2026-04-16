
using Microsoft.EntityFrameworkCore.Proxies;
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
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
Console.WriteLine("Database created succesfully!");

Console.WriteLine("Seeding database...");
var seeder = new TestDataGenerator(context);
seeder.GenerateConsultationTypes();
seeder.GenerateDoctors(100);
seeder.GeneratePatients(500);
seeder.GenerateConsultations(10000);
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

var consultationsWithDuration = context.Consultations
    .Join(context.ConsultationTypes,
    c => c.ConsultationTypeId,
    ct => ct.Id,
    (c, ct) => new { Consultation = c, Duration = ct.Duration })
    .ToList();

var overlapping = consultationsWithDuration
    .Where(a1 => consultationsWithDuration
        .Any(a2 =>
            a2.Consultation.Id != a1.Consultation.Id &&
            a2.Consultation.DoctorId == a1.Consultation.DoctorId &&
            a2.Consultation.TimeSlot.StartTime < a1.Consultation.TimeSlot.StartTime.Add(a1.Duration + TimeSpan.FromMinutes(5)) &&
            a2.Consultation.TimeSlot.StartTime.Add(a2.Duration + TimeSpan.FromMinutes(5)) > a1.Consultation.TimeSlot.StartTime))
    .ToList();

Console.WriteLine($"Found {overlapping.Count} overlapping consultations.");
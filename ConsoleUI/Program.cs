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
seeder.GeneratePatients(100);
seeder.GenerateConsultations(200);
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
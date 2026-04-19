using BlazorUI.Components;
using Infrastructure;
using UseCases.Interfaces;
using Facade.Interfaces;
using Infrastructure.Queries;
using Infrastructure.Repositories;
using UseCases.BookConsultation;
using UseCases.CompleteConsultation;
using UseCases.CancelConsultation;
using UseCases.ChangeConsultationType;
using UseCases.MarkArrived;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<DoctorsOfficeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String: 'DefaultConnection' not found.")));

//Transient creates a new instance every time the service is requesteed. Used for lightweight, stateless services.

// Scoped services are created once per client request (like connections).
// This is ideal for services that need to maintain state within a single request, such as repositories that interact with the database context.

// Singleton services are created the first time they are requested and then shared across all the following requests.
// This is best for services that maintain global state or are expensive to create, but should be used with caution in web applications to avoid unintended side effects.

// Register repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IConsultationTypeRepository, ConsultationTypeRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();

// Queries
builder.Services.AddScoped<IConsultationQueries, ConsultationQueries>();

// Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Use Cases
builder.Services.AddScoped<IBookConsultationUseCase, BookConsultationUseCase>();
builder.Services.AddScoped<ICancelConsultationUseCase, CancelConsultationUseCase>();
builder.Services.AddScoped<IChangeConsultationTypeUseCase, ChangeConsultationTypeUseCase>();
builder.Services.AddScoped<ICompleteConsultationUseCase, CompleteConsultationUseCase>();
builder.Services.AddScoped<IMarkArrivedUseCase, MarkArrivedUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

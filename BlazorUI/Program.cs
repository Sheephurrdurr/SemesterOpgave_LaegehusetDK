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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Register repositories and services
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IConsultationTypeRepository, ConsultationTypeRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();

// Queries
builder.Services.AddScoped<IConsultationQueries, ConsultationQueries>();

// Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Use Cases
builder.Services.AddScoped<BookConsultationUseCase>();
builder.Services.AddScoped<CancelConsultationUseCase>();
builder.Services.AddScoped<ChangeConsultationTypeUseCase>();
builder.Services.AddScoped<CompleteConsultationUseCase>();
builder.Services.AddScoped<MarkArrivedUseCase>();

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

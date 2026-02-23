using Domain.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;


using var context = new DoctorsOfficeContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

// Get consultationTypes from db test
var consultationTypes = context.ConsultationTypes.ToList();
Console.WriteLine("=== Consultationtypes in database ===");
foreach (var type in consultationTypes)
{
    Console.WriteLine($"{type.Name} - Duration: {type.Duration.TotalMinutes} min.");
}

Console.WriteLine(); // Some room in da console
Console.WriteLine();

// CREATE & READ 
var doctor = new Doctor("Anders Hansen");
var patient = new Patient("Henriette Gylling");

context.Doctors.Add(doctor);
context.Patients.Add(patient);
context.SaveChanges();

var doctors = context.Doctors.ToList();
var patients = context.Patients.ToList();

foreach(var horse in doctors) // What? You want me to use "doc"? No way, man. 
{
    Console.WriteLine($"Doctor's Name: {horse.Name},");
}

foreach(var cat in patients) // Yes, it's cat. What of it?
{
    Console.WriteLine($"Patient Name: {cat.Name},");
}

Console.WriteLine(); // Some room in da console
Console.WriteLine();

// CREATE & READ CONSULTATION
var regular = consultationTypes.OfType<RegularConsultation>().First();
var consultation1 = new Consultation(regular, doctor, patient, DateTime.Now.AddDays(4));
context.Consultations.Add(consultation1);
context.SaveChanges();

// EndTime computed property demonstrated here 
Console.WriteLine($"{consultation1.StartTime} - {consultation1.EndTime}. Type: {consultation1.ConsultationType.Name} consultation, with {patient.Name}. Doctor {doctor.Name}");

Console.WriteLine(); // Some room in da console
Console.WriteLine();

// UPDATE 
var consultation2 = context.Consultations
    .Include(x => x.ConsultationType)
    .FirstOrDefault(x => x.DoctorId == doctor.Id);

Console.WriteLine($"Original consultation type: {consultation2.ConsultationType.Name}");

var vaccination = consultationTypes.OfType<Vaccination>().First();

consultation2.ChangeConsultationType(vaccination, DateTime.Now.AddDays(3));
context.SaveChanges();

Console.WriteLine("ChangeConsultationType() has run.");
Console.WriteLine($"New consultation type: {consultation2.ConsultationType.Name}");

Console.WriteLine(); // Some room in da console
Console.WriteLine();

// DELETE
var consultationsBefore = context.Consultations.ToList();
Console.WriteLine($"Found {consultationsBefore.Count} consultation in database.");

context.Consultations.Remove(consultation2);
context.SaveChanges();
Console.WriteLine("Consultation has been deleted. Changes saved.");

var consultationsAfter = context.Consultations.ToList();
Console.WriteLine($"Found: {consultationsAfter.Count} consultations in database.");


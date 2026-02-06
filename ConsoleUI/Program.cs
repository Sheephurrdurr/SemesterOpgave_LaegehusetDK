using Domain.Entities;

var doctor = new Doctor();
var patient = new Patient();
var consultationType  = new Vaccination();
var startTime = DateTime.Now.AddDays(1);

var consultation = new Consultation(consultationType, doctor, patient, startTime);

var newConsultationType = new RegularConsultation();
consultation.ChangeConsultationType(newConsultationType);
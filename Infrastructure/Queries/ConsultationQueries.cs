using Facade.Interfaces;
using Facade.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
   
    public class ConsultationQueries : IConsultationQueries
    {
        private readonly DoctorsOfficeContext _context;
        public ConsultationQueries(DoctorsOfficeContext context) 
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ConsultationTypeDto>> GetAllConsultationTypesAsync()
        {
            var consultationTypes = await _context.ConsultationTypes
                .Select(ct => new ConsultationTypeDto(ct.Id, ct.Name, ct.Duration))
                .ToListAsync();
            return consultationTypes;
        }

        public async Task<IReadOnlyList<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors
                .Select(d => new DoctorDto(d.Id, d.Name))
                .ToListAsync();
            return doctors;
        }

        public async Task<IReadOnlyList<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto(p.Id, p.Name))
                .ToListAsync();
            return patients;
        }

        public async Task<IReadOnlyList<ConsultationDto>> GetTodaysConsultationsAsync()
        {
            var todaysConsultations = await _context.Consultations
                .Where(c => c.TimeSlot.StartTime.Date == DateTime.Today)
                .Join(_context.ConsultationTypes,
                    c => c.ConsultationTypeId,
                    ct => ct.Id,
                    (c, ct) => new { Consultation = c, ConsultationType = ct })
                .Join(_context.Doctors,
                    x => x.Consultation.DoctorId,
                    d => d.Id,
                    (x, d) => new { x.Consultation, x.ConsultationType, Doctor = d})
                .Join(_context.Patients,
                    x => x.Consultation.PatientId,
                    p => p.Id,
                    (x, p) => new { x.Consultation, x.ConsultationType, x.Doctor, Patient = p })
                .Select(c => new ConsultationDto(
                        c.Consultation.Id,
                        c.Doctor.Name,
                        c.Patient.Name,
                        c.ConsultationType.Name,
                        c.Consultation.TimeSlot.StartTime,
                        c.Consultation.TimeSlot.EndTime
                    ))
                .ToListAsync();

            return todaysConsultations;
        }
    }
}

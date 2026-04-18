using System;
using System.Collections.Generic;
using System.Text;
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

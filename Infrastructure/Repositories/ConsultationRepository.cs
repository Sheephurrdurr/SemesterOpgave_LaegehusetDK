using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UseCases.Interfaces;

namespace Infrastructure.Repositories
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly DoctorsOfficeContext _context;
        public ConsultationRepository(DoctorsOfficeContext context) 
        {
            _context = context;
        }
        public async Task<Consultation?> GetByIdAsync(Guid id)
        {
            return await _context.Consultations
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.Consultations
                .Where(c => c.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consultation>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.Consultations
                .Where(c => c.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task AddAsync(Consultation consultation)
        {
            await _context.Consultations.AddAsync(consultation);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

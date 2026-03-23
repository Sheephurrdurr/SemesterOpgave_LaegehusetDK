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
            var consultation = _context.Consultations
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId)
        {

        }

        public async Task<IEnumerable<Consultation>> GetByDoctorIdAsync(Guid doctorId)
        {

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

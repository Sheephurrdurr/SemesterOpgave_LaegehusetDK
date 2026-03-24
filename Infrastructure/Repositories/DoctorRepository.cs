using Domain.Entities;
using UseCases.Interfaces;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorsOfficeContext _context;

        public DoctorRepository(DoctorsOfficeContext context)
        {
            _context = context;
        }
        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await _context.Doctors
                .FindAsync(id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors
               .AddAsync(doctor);
        }

        public async Task SaveAsync()
        {
            await _context
                .SaveChangesAsync();
        }
    }
}

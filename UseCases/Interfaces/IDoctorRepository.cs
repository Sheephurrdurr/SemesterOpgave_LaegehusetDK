using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<Doctor?> GetByIdAsync(Guid id);

        public Task AddAsync(Doctor doctor);

        public Task SaveAsync();
    }
}

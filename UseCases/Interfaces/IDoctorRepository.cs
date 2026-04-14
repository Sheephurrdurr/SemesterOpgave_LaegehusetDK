using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(Guid id);

        Task AddAsync(Doctor doctor);
    }
}

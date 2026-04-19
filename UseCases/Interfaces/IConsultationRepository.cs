
using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IConsultationRepository
    {
        Task<Consultation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Consultation>> GetByDoctorIdAsync(Guid doctorId);
        Task<IEnumerable<Consultation>> GetByDoctorIdAndDateAsync(Guid doctorId, DateTime date);
        Task AddAsync(Consultation consultation);
    }
}

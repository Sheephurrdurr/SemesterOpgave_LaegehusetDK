
using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IConsultationRepository
    {
        public Task<Consultation?> GetByIdAsync(Guid id);

        public Task<IEnumerable<Consultation>> GetByPatientIdAsync(Guid patientId);

        public Task<IEnumerable<Consultation>> GetByDoctorIdAsync(Guid doctorId);

        public Task AddAsync(Consultation consultation);

    }
}

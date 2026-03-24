using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IConsultationTypeRepository
    {
        public Task<ConsultationType?> GetByIdAsync(Guid id);
    }
}

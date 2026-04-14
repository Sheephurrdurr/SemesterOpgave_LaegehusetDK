using Domain.Entities;

namespace UseCases.Interfaces
{
    public interface IConsultationTypeRepository
    {
        Task<ConsultationType?> GetByIdAsync(Guid id);
    }
}

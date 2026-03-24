using Domain.Entities;
using UseCases.Interfaces;

namespace Infrastructure.Repositories
{
    public class ConsultationTypeRepository : IConsultationTypeRepository
    {
        private readonly DoctorsOfficeContext _context;

        public ConsultationTypeRepository(DoctorsOfficeContext context)
        {
            _context = context;
        }

        public async Task<ConsultationType?> GetByIdAsync(Guid id)
        {
            return await _context.ConsultationTypes
                .FindAsync(id); 
        }
    }
}

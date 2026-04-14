using Facade.DTOs;
using UseCases.Interfaces;
using Facade.Interfaces;

namespace UseCases.ChangeConsultationType
{
    public class ChangeConsultationTypeUseCase : IChangeConsultationTypeUseCase
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IConsultationTypeRepository _consultationTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeConsultationTypeUseCase(

            IConsultationRepository consultationRepository,
            IConsultationTypeRepository consultationTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _consultationRepository = consultationRepository;
            _consultationTypeRepository = consultationTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(ChangeConsultationTypeRequest request) // No async void, use Task instead. Void eats up any exceptions thrown inside the method, making it harder to debug and handle errors properly.
        {
            var consultation = await _consultationRepository.GetByIdAsync(request.ConsultationId);
            var newConsultationType = await _consultationTypeRepository.GetByIdAsync(request.ConsultationTypeId);

            if (consultation == null)
                throw new ArgumentException("Consultation not found");
            if (newConsultationType is null) 
                throw new ArgumentException("Consultation Type not found");

            consultation.ChangeConsultationType(request.ConsultationTypeId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

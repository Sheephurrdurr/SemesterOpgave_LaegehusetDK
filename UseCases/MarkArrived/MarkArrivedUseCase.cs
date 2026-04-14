using System;
using System.Collections.Generic;
using System.Text;
using Facade.DTOs;
using UseCases.Interfaces;

namespace UseCases.MarkArrived
{
    public class MarkArrivedUseCase
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkArrivedUseCase(

            IConsultationRepository consultationRepository,
            IUnitOfWork unitOfWork)
        {
            _consultationRepository = consultationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(MarkArrivedRequest request) // No async void, use Task instead. Void eats up any exceptions thrown inside the method, making it harder to debug and handle errors properly.
        {
            var consultation = await _consultationRepository.GetByIdAsync(request.Id);

            // Validate if newly requested data is actually there
            if (consultation is null) throw new ArgumentException("Consultation not found");

            consultation.MarkArrived();

            // Save the newly created consultation to the database
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

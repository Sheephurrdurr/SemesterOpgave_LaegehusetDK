using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UseCases.BookConsultation;
using Facade.DTOs;
using UseCases.Interfaces;

namespace UseCases.CancelConsultation
{

    public class CancelConsultationUseCase
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelConsultationUseCase(
 
            IConsultationRepository consultationRepository,
            IUnitOfWork unitOfWork)
        {
            _consultationRepository = consultationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CancelConsultationRequest request) // No async void, use Task instead. Void eats up any exceptions thrown inside the method, making it harder to debug and handle errors properly.
        {
            var consultation = await _consultationRepository.GetByIdAsync(request.Id);

            // Validate if newly requested data is actually there
            if (consultation is null) throw new ArgumentException("Consultation not found");
           
           consultation.Cancel();

            // Save the newly created consultation to the database
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

            

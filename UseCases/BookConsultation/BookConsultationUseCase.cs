using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UseCases.Interfaces;
using Facade.DTOs;

namespace UseCases.BookConsultation
{
    public class BookConsultationUseCase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IConsultationTypeRepository _consultationTypeRepository;
        private readonly IConsultationRepository _consultationRepository;
        
        private readonly IUnitOfWork _unitOfWork;

        public BookConsultationUseCase(
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IConsultationTypeRepository consultationTypeRepository,
            IConsultationRepository consultationRepository,
            IUnitOfWork unitOfWork)

        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _consultationTypeRepository = consultationTypeRepository;
            _consultationRepository = consultationRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<BookConsultationResponse> ExecuteAsync(BookConsultationRequest request) 
        {
            // Get needed entities
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            var consultationType = await _consultationTypeRepository.GetByIdAsync(request.ConsultationTypeId);

            // Validate if newly requested data is actually there
            if (doctor is null) throw new ArgumentException("Doctor not found");
            if (patient is null) throw new ArgumentException("Patient not found");
            if (consultationType is null) throw new ArgumentException("Consultation Type not found");

            // Create consultation with the fetched data - all BL is located in Domain
            var consultation = new Consultation(consultationType, doctor, patient, request.StartTime);

            // Save the newly created consultation to the database
            await _consultationRepository.AddAsync(consultation);
            await _unitOfWork.SaveChangesAsync();

            // Return result
            return new BookConsultationResponse
            {
                ConsultationId = consultation.Id,
                Message = $"Consultation booked succesfully for {patient.Name} with {doctor.Name} on {request.StartTime}"
            };
        }

    }
}

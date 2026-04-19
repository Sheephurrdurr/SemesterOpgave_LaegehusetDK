using Domain.Entities;
using UseCases.Interfaces;
using Facade.DTOs;
using Facade.Interfaces;
using Domain.ValueObjects;

namespace UseCases.BookConsultation
{
    public class BookConsultationUseCase : IBookConsultationUseCase
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
        // CQS pattern: Command Query Separation - this is a command, as it changes state in the system (creates a new consultation)
        // BUUUT it also returns data, which is a 'no no' in pure CQS, but we can allow it for simplicity here.
        // In a more cool and sunglasses wearing version of this system, we could separate the command (which just executes the booking) from a query (which retrieves the details of the booked consultation).
        public async Task<BookConsultationResponse> ExecuteAsync(BookConsultationRequest request) 
        {
            // Get needed entities
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            var consultationType = await _consultationTypeRepository.GetByIdAsync(request.ConsultationTypeId);

            // Validate if newly requested data is even there
            if (doctor is null) throw new ArgumentException("Doctor not found");
            if (patient is null) throw new ArgumentException("Patient not found");
            if (consultationType is null) throw new ArgumentException("Consultation Type not found");

            var existingConsultations = await _consultationRepository.GetByDoctorIdAndDateAsync(doctor.Id, request.StartTime);

            var newTimeSlot = new TimeSlot(request.StartTime, request.StartTime.Add(consultationType.Duration));

            // Check for overlapping consultations via TimeSlot VO
            if (existingConsultations.Any(c => c.TimeSlot.OverlapsWith(newTimeSlot)))
            {
                throw new InvalidOperationException("Tiden er ikke ledig.");
            }

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

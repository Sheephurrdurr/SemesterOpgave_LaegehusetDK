using System.Net.Http.Headers;

namespace Domain.Entities
{
    // Coding association between Doctor and Patient, as a Doctor can have multiple Patients and a Patient can have multiple Doctors.
    public class Consultation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ConsultationType ConsultationType { get; private set; }
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; init; } // init allows setting the Patient only during object initialization, ensuring immutability after creation.
        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime EndTime { get; private set; }
        
        public Consultation(
                            ConsultationType consultationType,
                            Doctor doctor,
                            Patient patient,
                            DateTime startTime)
        {
            // Pre-condition checks
            ValidateStartTime(startTime);

            // Assignments
            ConsultationType = consultationType;
            Doctor = doctor;
            Patient = patient;
            StartTime = startTime;
            Duration = consultationType.Duration;
            EndTime = startTime + consultationType.Duration;
        }

        public void ChangeConsultationType(ConsultationType newType, DateTime startTime)
        {
            ConsultationType = newType;
            Duration = newType.Duration;
        }

        public void ChangeStartTime(DateTime newStartTime)
        {
            if (StartTime < DateTime.Now)
            {
                throw new ArgumentException("Start time cannot be in the past.");
            }
            ValidateStartTime(newStartTime);
            StartTime = newStartTime;
        }

        private void ValidateStartTime(DateTime startTime)
        {
            if (startTime < DateTime.Now)
            {
                throw new ArgumentException("Start time cannot be in the past.");
            }
        }
    }
}

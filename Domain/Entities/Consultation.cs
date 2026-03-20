using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Entities
{
    public class Consultation
    {
        public Guid Id { get; private set; }
        public Guid ConsultationTypeId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public Status Status { get; private set; }
        public TimeSlot TimeSlot {  get; private set; }
        
        public Consultation(
                            ConsultationType consultationType,
                            Doctor doctor,
                            Patient patient,
                            DateTime startTime)
        {
            // Assignments
            Id = Guid.NewGuid();
            ConsultationTypeId = consultationType.Id;
            DoctorId = doctor.Id;
            PatientId = patient.Id;
            Status = Status.Planned;
            TimeSlot = new TimeSlot(startTime, startTime + consultationType.Duration);
        }

        protected Consultation() { }

        public void ChangeConsultationType(Guid newConsultationTypeId)
        {
            ConsultationTypeId = newConsultationTypeId;
        }

        public void ChangeStartTime(TimeSlot newTimeSlot)
        {   
            TimeSlot = newTimeSlot;
        }

    }
}

using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Entities
{
    public class Consultation
    {
        public Guid Id { get; private set; }
        public Guid ConsultationTypeId { get; private set; } // No navigation properties, because we're keeping our aggregate roots clean.
                                                             // If we need to access the related entities, we can do that in the application layer with the repositories.
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public Status Status { get; private set; }
        // TimeSlot and database indexing note:
        // TimeSlot was supposed to be indexed, but due to EF Core being a little iffy with complex properties, I've decided not to.
        // The issue could be solved with some raw sql migrations, or by going directly into SSMS and creating the index there.
        public TimeSlot TimeSlot { get; private set; }
        public string? Note { get; private set; }

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

        public void Cancel() // Typical command, which does ... something. As opposed to a query, which just returns data.
                             // CQS says that commands should not return data, but in this case we throw exceptions if the command is invalid. 
        {
            if (Status == Status.Cancelled)
                throw new InvalidOperationException("Consultation is already cancelled.");

            if (Status == Status.Completed)
                throw new InvalidOperationException("Cannot cancel a completed consultation.");
            
            Status = Status.Cancelled;
        }

        public void MarkArrived()
        {
            if (Status == Status.Cancelled)
                throw new InvalidOperationException("Cannot mark a cancelled consultation as arrived.");

            Status = Status.Arrived;
        }

        public void Complete(string note)
        {
            if (Status == Status.Completed) 
                throw new InvalidOperationException("Consultation is already completed.");

            if (Status == Status.Cancelled)
                throw new InvalidOperationException("Cannot complete a cancelled consultation.");

            if (Status != Status.Arrived)
                throw new InvalidOperationException("Consultation must be marked as arrived before it can be completed.");

            Note = note;
            Status = Status.Completed;
        }
    }
}

using Domain.Entities;

namespace Domain.Scheduling
{
    public interface ISchedulingStrategy
    {
        DateTime? FindAvailableSlot(
            IEnumerable<Consultation> existingConsultations,
            DateTime preferredTime,
            TimeSpan duration);
    }
}

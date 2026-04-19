using Domain.Entities;

namespace Domain.Scheduling
{
    public class EarliestAvailableStrategy : ISchedulingStrategy
    {
        public DateTime? FindAvailableSlot(
            IEnumerable<Consultation> existingConsultations,
            DateTime preferredTime,
            TimeSpan duration)
        {
            var sortedConsultations = existingConsultations
                .OrderBy(c => c.TimeSlot.StartTime)
                .ToList();

            foreach(var consultation in sortedConsultations)
            {
                if (consultation.TimeSlot.OverlapsWith(preferredTime, preferredTime.Add(duration)))
                {
                    preferredTime = consultation.TimeSlot.EndTime;
                }
            }

            var workDayEnd = preferredTime.AddHours(16);

            if (preferredTime + duration > workDayEnd) 
            { 
                return null; 
            }

            return preferredTime;
        }
    }
}

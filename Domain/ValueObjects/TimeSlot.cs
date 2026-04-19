
namespace Domain.ValueObjects
{
    public class TimeSlot
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration => EndTime - StartTime;

        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            if (startTime < DateTime.Today)
                throw new ArgumentException("Start time cannot be in the past.");

            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time.");

           

            StartTime = startTime;
            EndTime = endTime;
        }

        public bool OverlapsWith(TimeSlot other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        public override string ToString()
        {
            return $"{StartTime} - {EndTime}";
        }
    }
}
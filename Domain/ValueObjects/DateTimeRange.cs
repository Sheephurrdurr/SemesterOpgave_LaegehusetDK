namespace Domain.ValueObjects
{
    public class DateTimeRange
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TimeSpan Duration => EndTime - StartTime;

        public DateTimeRange(DateTime startTime, DateTime endTime)
        {
            if (startTime < DateTime.Now)
                throw new ArgumentException("Start time cannot be in the past.");

            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time.");

            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
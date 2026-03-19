using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class DateTimeRange
    {
        public DateTimeRange Period { get; private set; }
        public Consultation(DateTime startTime,  DateTime endTime, TimeSpan duration)
        {
            if (startTime < DateTime.Now)
            {
                throw new ArgumentException("Start time cannot be in the past.");
            }
            if (endTime <= startTime)
            {
                throw new ArgumentException("End time must be after start time.");
            }
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentException("Duration must be a positive time span.");
            }
            if (endTime - startTime != duration)
            {
                throw new ArgumentException("The duration does not match the difference between start and end times.");
            }
            Period = new DateTimeRange { StartTime = startTime, EndTime = endTime };
        }
    }
}

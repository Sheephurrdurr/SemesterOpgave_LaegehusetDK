namespace Domain.Entities
{
    // 
    public abstract class ConsultationType
    {
        public Guid Id { get; protected init; }
        public TimeSpan Duration { get; protected set; }

        protected ConsultationType(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
                throw new ArgumentException("Duration must be positive.");

            Id = Guid.NewGuid();
            Duration = duration;
        }
        protected ConsultationType() { }
    }
    
    public class RegularConsultation : ConsultationType
    {
        public RegularConsultation() : base(TimeSpan.FromMinutes(20)) { }
    }

    public class  Vaccination : ConsultationType
    {
        public Vaccination() : base(TimeSpan.FromMinutes(10)) { }
    }

    public class PerscriptionRenewal : ConsultationType
    {
        public PerscriptionRenewal() : base(TimeSpan.FromMinutes(10)) { }
    }

    public class CounselingSession : ConsultationType
    {
        public CounselingSession() : base(TimeSpan.FromMinutes(15)) { }
    }
}

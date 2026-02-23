namespace Domain.Entities
{
    // 
    public abstract class ConsultationType
    {
        public Guid Id { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Name { get; private set; }

        protected ConsultationType(string name, TimeSpan duration) // Has to be protected cuz u would never call it anyway. Use the subclasses.
        {
            if (duration <= TimeSpan.Zero)
                throw new ArgumentException("Duration must be positive.");

            Id = Guid.NewGuid();
            Name = name;
            Duration = duration;
        }
        protected ConsultationType(Guid id, string name, TimeSpan duration) 
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
    
    public class RegularConsultation : ConsultationType
    {
        public RegularConsultation(Guid id) : base(id, "Regular", TimeSpan.FromMinutes(20)) { }
    }

    public class  Vaccination : ConsultationType
    {
        public Vaccination(Guid id) : base(id, "Vaccination", TimeSpan.FromMinutes(10)) { }
    }

    public class PerscriptionRenewal : ConsultationType
    {
        public PerscriptionRenewal(Guid id) : base(id, "Perscription Renewal", TimeSpan.FromMinutes(10)) { }
    }

    public class CounselingSession : ConsultationType
    {
        public CounselingSession(Guid id) : base(id, "Counseling", TimeSpan.FromMinutes(15)) { }
    }
}

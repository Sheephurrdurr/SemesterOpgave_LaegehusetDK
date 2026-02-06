namespace Domain.Entities
{
    public class ConsultationType
    {
        public TimeSpan Duration { get; protected set; }
    }

    public class RegularConsultation : ConsultationType
    {
        public RegularConsultation()
        {
            Duration = TimeSpan.FromMinutes(20);
        }
    }

    public class  Vaccination : ConsultationType
    {
        public Vaccination()
        {
            Duration = TimeSpan.FromMinutes(10);
        }
    }

    public class PerscriptionRenewal : ConsultationType
    {
        public PerscriptionRenewal()
        {
            Duration = TimeSpan.FromMinutes(10);
        }
    }

    public class CounselingSession : ConsultationType
    {
        public CounselingSession()
        {
            Duration = TimeSpan.FromMinutes(15);
        }
    }
}

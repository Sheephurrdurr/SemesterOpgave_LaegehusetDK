namespace Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();
    }
}

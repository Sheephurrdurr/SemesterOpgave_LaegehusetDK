namespace Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();

        public Patient(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("Doctor must have a name.");

            Id = Guid.NewGuid();
            Name = name;
        }

        protected Patient() { } // For EF Core
    }
}

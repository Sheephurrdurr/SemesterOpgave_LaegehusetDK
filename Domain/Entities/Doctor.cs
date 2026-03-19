namespace Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();
        public Doctor(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Patient must have a name.");
            }

            Id = Guid.NewGuid();
            Name = name;
        }
        public Doctor(Guid id, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Patient must have a name.");
            }

            Id = id;
            Name = name;
        }


        protected Doctor() { } // EF Core uses this constructor to create the object via reflection. It cant use the standard constructor, cuz it doesnt have the arguments yet. 
    }
}

namespace Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();
        public Doctor(string name) : this(Guid.NewGuid(), name) { } // Call constructor with these args. Default constructor for "daily use". DRY af. 
        public Doctor(Guid id, string name) // Constructor for test data seeding. Allows controlling the Guid of created objects
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Doctor must have a name.");
            }

            Id = id;
            Name = name;
        }

        protected Doctor() { } // EF Core uses this constructor to create the object via reflection. It cant use the standard constructor, cuz it doesnt have the arguments yet. 
    }
}

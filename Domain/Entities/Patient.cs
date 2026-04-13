namespace Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cpr { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();
        public Patient(string name, string cpr) : this(Guid.NewGuid(), name, cpr) { } // Call constructor with these args. Default constructor for "daily use". DRY af. 

        public Patient(Guid id, string name, string cpr) // Constructor for test data seeding. Allows controlling the Guid of created objects
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Patient must have a name.");

            Id = id;
            Name = name;
            ValidateCpr(cpr);
            Cpr = cpr;
        }

        protected Patient() { } // For EF Core

        public void ValidateCpr(string cpr)
        {
            if (string.IsNullOrEmpty(cpr))
            {
                throw new ArgumentNullException("CPR can't be empty.");
            }

            if (cpr.Length != 10)
            {
                throw new ArgumentException("CPR must be 10 digits.");
            }

            if (!cpr.All(char.IsDigit))
            {
                throw new ArgumentException("CPR must contain only digits.");
            }
        }
    }
}

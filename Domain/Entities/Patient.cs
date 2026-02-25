namespace Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cpr { get; private set; }
        public ICollection<Consultation> Consultations { get; private set; } = new List<Consultation>();
        public Patient(string name, string cpr)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Doctor must have a name.");

            Id = Guid.NewGuid();
            Name = name;
            ValidateCpr(cpr);
            Cpr = cpr;
        }
        public Patient(Guid id, string name, string cpr)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("Doctor must have a name.");

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

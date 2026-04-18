using Domain.Entities;
using Infrastructure;

namespace ConsoleUI
{
    // Test data generation script for populating the database with dummy data for testing and performance measurement purposes.
    // Seeding is done from Program.cs, but the actual logic is encapsulated in this class for that sweet separation of concerns.
    // Everything is fairly straight forward here, I should be able to pick this up. You hear that, me? You've fallen off, if you cant read this code.
    public class TestDataGenerator
    {
        private readonly DoctorsOfficeContext _context;
        private readonly Random _random = new Random();

        public TestDataGenerator(DoctorsOfficeContext context)
        {
            _context = context;
        }

        public void GenerateConsultationTypes()
        {
            List<ConsultationType> consultationTypes = new List<ConsultationType>();

            consultationTypes.Add(new RegularConsultation(Guid.NewGuid()));
            consultationTypes.Add(new Vaccination(Guid.NewGuid()));
            consultationTypes.Add(new PerscriptionRenewal(Guid.NewGuid()));
            consultationTypes.Add(new CounselingSession(Guid.NewGuid()));

            _context.AddRange(consultationTypes);
            _context.SaveChanges();
        }

        public void GenerateDoctors(int count)
        {
            List<Doctor> doctors = new List<Doctor>();

            for (int i = 0; i < count; i++)
            {
                doctors.Add(new Doctor($"Doctor{i}"));

                if (i % (count / 10) == 0)
                {
                    Console.WriteLine($"Generated {i}/{count} doctors...");
                }
            }
            _context.AddRange(doctors);
            _context.SaveChanges();
        }

        public void GeneratePatients(int count)
        {
            List<Patient> patiens = new List<Patient>();

            for (int i = 0; i < count; i++)
            {
                patiens.Add(new Patient($"Patient{i}", CprGenerator()));

                if (i % (count / 10) == 0)
                {
                    Console.WriteLine($"Generated {i}/{count} patients...");
                }
            }
            _context.AddRange(patiens);
            _context.SaveChanges();
        }

        public void GenerateConsultations(int count)
        {
            var doctors = _context.Doctors.ToList();
            var patients = _context.Patients.ToList();

            var consultationTypes = _context.ConsultationTypes.ToList();

            List<Consultation> consultations = new List<Consultation>();

            for (int i = 0; i < count; i++)
            {
                var consultation1 = new Consultation(
                    consultationTypes[_random.Next(consultationTypes.Count)],
                    doctors[_random.Next(doctors.Count)],
                    patients[_random.Next(patients.Count)],
                    DateTime.Today.AddDays(_random.Next(1,30)).Date.AddHours(_random.Next(8,15))
                );

                if (i % (count / 10) == 0)
                {
                    Console.WriteLine($"Generated {i}/{count} consultations...");
                }

                consultations.Add( consultation1 );
            }
            _context.AddRange(consultations);
            _context.SaveChanges();
        }

        public string CprGenerator()
        {
            List<int> cprInts = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                int randomInt = _random.Next(0,10);
                cprInts.Add(randomInt);
            }

            return string.Join("", cprInts);

        }
    }
}

using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class TestDataGenerator
    {
        private readonly DoctorsOfficeContext _context;
        private readonly Random _random = new Random();

        public TestDataGenerator(DoctorsOfficeContext context)
        {
            _context = context;
        }

        public void GenerateDoctors(int count)
        {
            List<Doctor> doctors = new List<Doctor>();

            for (int i = 0; i < count; i++)
            {
                doctors.Add(new Doctor(NameGenerator()));

                if (i % 100 == 0)
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
                patiens.Add(new Patient(NameGenerator(), CprGenerator()));

                if (i % 100 == 0)
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
                    DateTime.Now.AddDays(_random.Next(1,30))
                );

                consultations.Add( consultation1 );
            }
            _context.AddRange(consultations);
        }

        public string NameGenerator() // It's just a number generator that converts them to string. 
        {
            int rng = _random.Next(999);

            string name = rng.ToString();

            return name;
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

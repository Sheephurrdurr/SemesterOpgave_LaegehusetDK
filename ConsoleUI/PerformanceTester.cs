using System.Diagnostics;
using Infrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUI
{
    public class PerformanceTester
    {
        private readonly DoctorsOfficeContext _context;

        public PerformanceTester(DoctorsOfficeContext context)
        {
            _context = context;
        }

        // Eager loading with Include() is generally the most efficient way to load related data when you know you'll need it,
        // because it retrieves all the necessary data in a single query.
        public void TestEagerLoading()
        {
            var sw = new Stopwatch();
            sw.Start();
            List<Doctor> doctors = _context.Doctors
                .Include(d => d.Consultations)
                .ToList();
            sw.Stop();
            Console.WriteLine($"Eager loading took {sw.ElapsedMilliseconds} ms | doctors: {doctors.Count} | consultations: {doctors.Sum(d => d.Consultations.Count)}");
        }

        // NOTE: Lazy Loading is not enabled by default in EF Core.
        // Without LazyLoadingProxies, test does not generate N+1 queries.
        // In a 'real' N+1 scenario with 100 doctors, EF would generate 101 queries.
        // 1 query for doctors + 1 query per doctor for their consultations.
        // This is why Eager Loading with Include() is preferred.
        public void TestLazyLoadingNPlus1()
        {
            var sw = new Stopwatch();
            sw.Start();

            List<Doctor> doctors = _context.Doctors
                .ToList();

            foreach (Doctor doctor in doctors)
            {
                Console.WriteLine($"Consultation: {doctor.Consultations.Count}");
            }

            sw.Stop();
            //Console.WriteLine($"Lazy loading (N+1) took {sw.ElapsedMilliseconds} ms | doctors: {doctors.Count} | consultations: {doctors.Sum(d => d.Consultations.Count)}");
        }

        // Explicit loading allows you to load related data on demand, but it can lead to a whole lot of queries, if not used carefully.
        // It's more efficient than lazy loading in some cases,
        // but still generally less efficient than eager loading with Include() for scenarios where you know you'll need the related data.
        // So in conclusion: it kinda sucks.
        public void TestExplicitLoading()
        {
            var satudarahWatch = new Stopwatch();
            satudarahWatch.Start();

            List<Doctor> doctors = _context.Doctors
                .ToList();

            foreach(Doctor doctor in doctors)
            {
                _context.Entry(doctor).Collection(d => d.Consultations).Load();
            }

            satudarahWatch.Stop();
            Console.WriteLine($"Explicit Loading took {satudarahWatch.ElapsedMilliseconds} ms | doctors: {doctors.Count} | consultations: {doctors.Sum(d => d.Consultations.Count)}");
        }

        // Raw SQL queries can be used for complex queries or stuff where performance is very important,
        // but they bypass EF's change tracking (it keeps track of what it's been doing) and other features.
        // but sometimes EF Core sucks at generating efficient SQL for certain queries,
        // and raw SQL can be a way to optimize performance in those cases.
        public void TestRawSql()
        {
            var sw = new Stopwatch();
            sw.Start();

            var doctors = _context.Doctors
                .FromSqlRaw("SELECT * FROM Doctors")
                .Include(d => d.Consultations)
                .ToList();

            sw.Stop();
            Console.WriteLine($"Raw SQL with Include() took {sw.ElapsedMilliseconds} ms | doctors: {doctors.Count} | consultations: {doctors.Sum(d => d.Consultations.Count)}");
        }
    }
}

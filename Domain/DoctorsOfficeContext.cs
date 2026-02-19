using Domain.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DoctorsOfficeContext : DbContext
    {
        public DoctorsOfficeContext(DbContextOptions<DoctorsOfficeContext> options) : base(options)
        {
        }

        public DoctorsOfficeContext() { }

        public DbSet<Entities.Doctor> Doctors { get; set; }
        public DbSet<Entities.Patient> Patients { get; set;  }
        public DbSet<Entities.ConsultationType> ConsultationTypes { get; set; }
        public DbSet<Entities.Consultation> Consultations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureSqLite();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DoctorConfiguration()); // Add configs 
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultationConfiguration());
        }
    }
}

using Domain.Configs;
using Domain.Entities;
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
        public DbSet<Entities.Patient> Patients { get; set; }
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

            modelBuilder.Entity<RegularConsultation>().HasData(
                 new RegularConsultation(new Guid("4ADF9313-E990-4BFC-B186-A886179DA195"))
            );

            modelBuilder.Entity<Vaccination>().HasData(
               new Vaccination(new Guid("9E6F6390-2A8D-4144-9F3A-01ECA22F5FEA"))
            );

            modelBuilder.Entity<PerscriptionRenewal>().HasData(
            new PerscriptionRenewal(new Guid("CE3E1283-08AF-4CC4-8B2F-E29B63A7DE12"))
            );

            modelBuilder.Entity<CounselingSession>().HasData(
               new CounselingSession(new Guid("185A3999-762C-4D98-A601-5492B16D3E04"))
            );
        }
    }
}
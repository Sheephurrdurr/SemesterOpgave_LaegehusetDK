using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Configs
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.ToTable("Consultations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.ConsultationTypeId).IsRequired();
            builder.Property(x => x.DoctorId).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();

            builder.HasOne(x => x.ConsultationType)
                .WithMany()
                .HasForeignKey(x => x.ConsultationTypeId);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Consultations)
                .HasForeignKey(x => x.DoctorId);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Consultations)
                .HasForeignKey(x => x.PatientId);

            builder.Ignore(x => x.EndTime); // EndTime is calculated on the fly. No reason to save it, when we can just do that.

        }
    }
}

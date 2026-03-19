using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Configs
{
    public class ConsultationTypeConfiguration : IEntityTypeConfiguration<ConsultationType>
    {
        public void Configure(EntityTypeBuilder<ConsultationType> builder)
        {
            builder.ToTable("ConsultationTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever(); // Domain driven Id generation. That skank, EF, doesn't get to do it.

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Duration)
                .IsRequired();

            // TPH (Table Pr. Hierachy) discriminator. Control how the different types get listed in the database. 
            builder.HasDiscriminator<string>("Type")
                .HasValue<RegularConsultation>("Regular")
                .HasValue<Vaccination>("Vaccination")
                .HasValue<PerscriptionRenewal>("PerscriptionRenewal")
                .HasValue<CounselingSession>("Counseling");
        }
    }
}

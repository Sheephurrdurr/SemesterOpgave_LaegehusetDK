using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever(); // Force the db to NOT create an Id. That's handled by the entity itself. 
            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.ComplexProperty(x => x.TimeSlot, t => t.ToJson());
        }
    }
}

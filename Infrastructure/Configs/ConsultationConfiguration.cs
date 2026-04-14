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

            // Configure the TimeSlot as a complex property, because it's a value object.
            // This way EF will know to map the StartTime and EndTime properties of TimeSlot to columns in the Consultation table.
            builder.ComplexProperty(x => x.TimeSlot, ts =>
            {
                ts.Property(t => t.StartTime).HasColumnName("StartTime");
                ts.Property(t => t.EndTime).HasColumnName("EndTime");
            });

            builder.HasIndex("StartTime"); // Indexing the TimeSlot for faster queries when searching for consultations by time.
                                           // Basically, indexing is just making the database look up the value in a sorted list (the index) instead of scanning through all the records.
        }
    }
}

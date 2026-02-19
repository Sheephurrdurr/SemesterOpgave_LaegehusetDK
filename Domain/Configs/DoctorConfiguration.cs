using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Configs
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Entities.Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
           
        }
    }
}

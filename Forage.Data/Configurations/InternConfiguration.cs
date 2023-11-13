using Forage.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Data.Configurations
{
    public class InternConfiguration:IEntityTypeConfiguration<Intern>
    {
        public void Configure(EntityTypeBuilder<Intern> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.Surname).IsRequired()
            .HasMaxLength(30);
            builder.Property(x => x.BirthYear).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.CreatedAt)
              .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

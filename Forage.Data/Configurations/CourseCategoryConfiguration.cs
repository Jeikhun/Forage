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
    public class CourseCategoryConfiguration:IEntityTypeConfiguration<CourseCategory>
    {

        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.Property(x => x.Name).IsRequired()
           .HasMaxLength(70);
            builder.Property(x => x.CreatedAt)
          .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

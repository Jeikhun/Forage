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
    public class CourseLessonLevelConfiguration:IEntityTypeConfiguration<CourseLessonLevel>
    {

        public void Configure(EntityTypeBuilder<CourseLessonLevel> builder)
        {
            builder.Property(x => x.Name).IsRequired()
           .HasMaxLength(30);
            builder.Property(x => x.CreatedAt)
          .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

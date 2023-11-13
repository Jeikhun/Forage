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
    public class CourseConfiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.CourseDuration).IsRequired()
               .HasMaxLength(50);
            builder.Property(x => x.About).IsRequired()
               .HasMaxLength(500);
            builder.Property(x => x.ShortInfo).IsRequired()
              .HasMaxLength(200);
            builder.Property(x => x.Language).IsRequired();
            builder.Property(x => x.AboutImage).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.AboutVideo).IsRequired();
            builder.Property(x => x.AboutImageUrl).IsRequired();
            builder.Property(x => x.AboutVideoUrl).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.CourseCategoryId).IsRequired();
            builder.Property(x => x.CourseLevelId).IsRequired();
            builder.Property(x => x.CreatedAt)
              .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

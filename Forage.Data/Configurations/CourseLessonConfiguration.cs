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
    public class CourseLessonConfiguration : IEntityTypeConfiguration<CourseLesson>
    {
        public void Configure(EntityTypeBuilder<CourseLesson> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.CourseDuration).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Video).IsRequired();
            builder.Property(x => x.VideoUrl).IsRequired();
            builder.Property(x => x.LessonLine).IsRequired();
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.CourseLessonLevelId).IsRequired();
            builder.Property(x => x.CreatedAt)
              .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

﻿using Forage.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Data.Configurations
{
    public class QuestionConfiguration:IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.Title).IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.Text).IsRequired()
            .HasMaxLength(300);
            builder.Property(x => x.CreatedAt)
              .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

using Forage.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Data.Configurations
{
    public class HelpMessageConfiguration : IEntityTypeConfiguration<HelpMessage>
    {
        public void Configure(EntityTypeBuilder<HelpMessage> builder)
        {
            builder.Property(x => x.Email).IsRequired()
                .HasMaxLength(25);
            builder.Property(x => x.FirstName).IsRequired()
                .HasMaxLength(15);
            builder.Property(x => x.LastName).IsRequired()
                .HasMaxLength(15);
            builder.Property(x => x.Message).IsRequired()
                .HasMaxLength(400);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

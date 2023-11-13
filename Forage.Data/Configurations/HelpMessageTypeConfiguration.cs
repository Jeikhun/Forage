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
    public class HelpMessageTypeConfiguration:IEntityTypeConfiguration<HelpMessageType>
    {
        public void Configure(EntityTypeBuilder<HelpMessageType> builder)
        {
            builder.Property(x => x.Title).IsRequired()
                .HasMaxLength(70);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow.AddHours(4));
        }
    }
}

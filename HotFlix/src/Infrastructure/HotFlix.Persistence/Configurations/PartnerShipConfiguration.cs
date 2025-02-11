using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Configurations
{
    internal class PartnerShipConfiguration : IEntityTypeConfiguration<PartnerShip>
    {
        public void Configure(EntityTypeBuilder<PartnerShip> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}

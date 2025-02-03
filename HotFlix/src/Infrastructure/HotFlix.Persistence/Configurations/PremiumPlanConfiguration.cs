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
    internal class PremiumPlanConfiguration : IEntityTypeConfiguration<PremiumPlan>
    {
        public void Configure(EntityTypeBuilder<PremiumPlan> builder)
        {
            builder.Property(x => x.PlanName).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(x => x.Expires).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Quality).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}

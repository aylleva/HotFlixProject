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
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(x => x.RunningTime).IsRequired().HasColumnType("nvarchar(30)");
        }
    }
}

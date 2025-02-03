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
    internal class MovieActorsConfiguration : IEntityTypeConfiguration<MovieActors>
    {
        public void Configure(EntityTypeBuilder<MovieActors> builder)
        {
            builder.HasKey(ma => new { ma.MovieId,ma.ActorId });
        }
    }
}

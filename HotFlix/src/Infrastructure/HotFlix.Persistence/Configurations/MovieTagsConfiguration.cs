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
    internal class MovieTagsConfiguration : IEntityTypeConfiguration<MovieTags>
    {
        public void Configure(EntityTypeBuilder<MovieTags> builder)
        {
            builder.HasKey(mt => new {mt.MovieId,mt.TagId});
        }
    }
}

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
    internal class JobContactConfiguration : IEntityTypeConfiguration<JobContact>
    {
        public void Configure(EntityTypeBuilder<JobContact> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(256)");
        }
    }
}

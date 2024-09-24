using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class AgencyConfiguration : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Accounts).WithOne(x => x.Agency);
            builder.HasMany(x => x.Orders).WithOne(x => x.Agency);
        }
    }
}

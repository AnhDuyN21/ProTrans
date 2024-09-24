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
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.CustomerAccount).WithMany(x => x.Requests).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.ShipperAccount).WithMany(x => x.Requests).HasForeignKey(x => x.ShipperId);
            builder.HasMany(x => x.Documents).WithOne(x => x.Request);
            builder.HasOne(x => x.Order).WithOne(x => x.Request);
            builder.HasMany(x => x.Images).WithOne(x => x.Request);
        }
    }
}

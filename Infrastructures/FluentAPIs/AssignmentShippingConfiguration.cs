using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructures.FluentAPIs
{
    public class AssignmentShippingConfiguration : IEntityTypeConfiguration<AssignmentShipping>
    {
        public void Configure(EntityTypeBuilder<AssignmentShipping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Shipper)
                .WithMany(x => x.AssignmentShippings)
                .HasForeignKey(x => x.ShipperId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.AssignmentShippings
                ).HasForeignKey(x => x.OrderId);
        }
    }
}

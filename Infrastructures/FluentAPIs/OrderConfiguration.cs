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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.PaymentMethod)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PaymentId);

            builder.HasOne(x => x.Agency)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.AgencyId);

            builder.HasOne(x => x.Request)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.RequestId);

            builder.Property(d => d.TotalPrice)
                    .HasColumnType("decimal(18, 2)");


        }
    }
}

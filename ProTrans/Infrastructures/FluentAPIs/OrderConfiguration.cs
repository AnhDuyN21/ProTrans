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
            builder.HasOne(x => x.PaymentMethod).WithMany(x => x.Orders).HasForeignKey(x => x.PaymentId);
            builder.HasOne(x => x.Agency).WithMany(x => x.Orders).HasForeignKey(x => x.AgencyId);
            builder.HasOne(x => x.Request).WithOne(x => x.Order).HasForeignKey<Order>(x => x.RequestId);
            builder.HasOne(x => x.TransactionsHistory).WithOne(x => x.Order);
            builder.HasMany(x => x.Documents).WithOne(x => x.Order);
            builder.HasMany(x => x.FeedBacks).WithOne(x => x.Order);
            builder.HasMany(x => x.Images).WithOne(x => x.Order);
            builder.HasMany(x => x.Shippings).WithOne(x => x.Order);
        }
    }
}

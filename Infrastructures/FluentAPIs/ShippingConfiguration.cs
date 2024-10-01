using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Shippings)
                .HasForeignKey(x => x.ShipperId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Shippings
                ).HasForeignKey(x => x.OrderId);
        }
    }
}

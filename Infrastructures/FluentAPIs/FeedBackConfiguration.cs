using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.FeedBacks)
                .HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.FeedBacks)
                .HasForeignKey(x => x.OrderId);
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class QuotePriceConfiguration : IEntityTypeConfiguration<QuotePrice>
    {
        public void Configure(EntityTypeBuilder<QuotePrice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FirstLanguage)
                .WithMany(x => x.FirstLanguage_QuotePrice)
                .HasForeignKey(x => x.FirstLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SecondLanguage)
                .WithMany(x => x.SecondLanguage_QuotePrice)
                .HasForeignKey(x => x.SecondLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.PricePerPage)
                    .HasColumnType("decimal(18, 2)");
        }
    }
}

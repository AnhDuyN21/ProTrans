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
    public class QuotePriceConfiguration : IEntityTypeConfiguration<QuotePrice>
    {
        public void Configure(EntityTypeBuilder<QuotePrice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.FirstLanguage)
                .WithMany(x => x.FirstLanguage_QuotePrice)
                .HasForeignKey(x => x.FirstLanguageId);

            builder.HasOne(x => x.SecondLanguage)
                .WithMany(x => x.SecondLanguage_QuotePrice)
                .HasForeignKey(x => x.SecondLanguageId);

            builder.Property(d => d.PricePerPage)
                    .HasColumnType("decimal(18, 2)");
        }
    }
}

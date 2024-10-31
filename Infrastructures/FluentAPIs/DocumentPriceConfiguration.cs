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
    public class DocumentPriceConfiguration : IEntityTypeConfiguration<DocumentPrice>
    {
        public void Configure(EntityTypeBuilder<DocumentPrice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Document)
                .WithOne(x => x.DocumentPrice)
                .HasForeignKey<DocumentPrice>(x => x.DocumentId);
        }
    }
}

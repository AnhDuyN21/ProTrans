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
    public class ImageShippingConfiguration : IEntityTypeConfiguration<ImageShipping>
    {
        public void Configure(EntityTypeBuilder<ImageShipping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AssignmentShipping)
                .WithMany(x => x.ImageShippings)
                .HasForeignKey(x => x.AssignmentShippingId);

            builder.HasOne(x => x.Document)
                .WithOne(x => x.ImageShipping)
                .HasForeignKey<ImageShipping>(x => x.DocumentId);
        }
    }
}

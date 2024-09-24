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
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.ShipperId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Request)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.RequestId);
        }
    }
}

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
    public class DistanceConfiguration : IEntityTypeConfiguration<Distance>
    {
        public void Configure(EntityTypeBuilder<Distance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.TargetAgency)
                .WithMany(x => x.TargetAgency)
                .HasForeignKey(x => x.TargetAgencyId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RootAgency)
                          .WithMany(x => x.RootAgency)
                          .HasForeignKey(x => x.RootAgencyId)
                          .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.Value)
                    .HasColumnType("decimal(5, 2)");
        }
    }
}

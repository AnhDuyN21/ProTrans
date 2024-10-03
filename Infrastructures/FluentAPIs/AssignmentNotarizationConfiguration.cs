using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class AssignmentNotarizationConfiguration : IEntityTypeConfiguration<AssignmentNotarization>
    {
        public void Configure(EntityTypeBuilder<AssignmentNotarization> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Shipper)
                .WithMany(x => x.AssignmentNotarizations)
                .HasForeignKey(x => x.ShipperId);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.AssignmentNotarizations)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}

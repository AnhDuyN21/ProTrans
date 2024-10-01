using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class NotarizationConfiguration : IEntityTypeConfiguration<Notarization>
    {
        public void Configure(EntityTypeBuilder<Notarization> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Price)
            .HasColumnType("decimal(18, 2)");
        }
    }
}

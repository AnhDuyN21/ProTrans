using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class AssignmentTranslationConfiguration : IEntityTypeConfiguration<AssignmentTranslation>
    {
        public void Configure(EntityTypeBuilder<AssignmentTranslation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Translator)
                .WithMany(x => x.AssignmentTranslations)
                .HasForeignKey(x => x.TranslatorId);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.AssignmentTranslations)
                .HasForeignKey(x => x.DocumentId);

            builder.Property(x => x.Deadline)
                 .HasColumnType("date");
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Request)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.RequestId);

            builder.HasOne(x => x.Notarization)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.NotarizationId);

            builder.HasOne(x => x.DocumentType)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.DocumentTypeId);

            builder.HasOne(x => x.FirstLanguage)
                .WithMany(x => x.FirstLanguage_Attachment)
                .HasForeignKey(x => x.FirstLanguageId);

            builder.HasOne(x => x.SecondLanguage)
                .WithMany(x => x.SecondLanguage_Attachment)
                .HasForeignKey(x => x.SecondLanguageId);

            builder.Property(d => d.Price)
                    .HasColumnType("decimal(18, 2)");
        }
    }
}

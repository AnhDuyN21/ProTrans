using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Request)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.RequestId);

            builder.HasOne(x => x.Notarization)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.NotarizationId);

            builder.HasOne(x => x.DocumentType)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.DocumentTypeId);

            builder.HasOne(x => x.FirstLanguage)
                .WithMany(x => x.FirstLanguage_Document)
                .HasForeignKey(x => x.FirstLanguageId);

            builder.HasOne(x => x.SecondLanguage)
                .WithMany(x => x.SecondLanguage_Document)
                .HasForeignKey(x => x.SecondLanguageId);

            builder.HasOne(x => x.Attachment)
                .WithOne(x => x.Document)
                .HasForeignKey<Document>(x => x.AttachmentId);
        }
    }
}

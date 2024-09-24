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
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order).WithMany(x => x.Documents).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Request).WithMany(x => x.Documents).HasForeignKey(x => x.RequestId);
            builder.HasOne(x => x.Notarization).WithMany(x => x.Documents).HasForeignKey(x => x.NotarizationId);
            builder.HasOne(x => x.DocumentType).WithMany(x => x.Documents).HasForeignKey(x => x.DocumentTypeId);
            builder.HasMany(x => x.AssignmentTranslations).WithOne(x => x.Document);
            builder.HasMany(x => x.AssignmentNotarizations).WithOne(x => x.Document);
            builder.HasOne(x => x.FirstLanguage).WithMany(x => x.Documents).HasForeignKey(x => x.FirstLanguageId);
            builder.HasOne(x => x.SecondLanguage).WithMany(x => x.Documents).HasForeignKey(x => x.SecondLanguageId);

        }
    }
}

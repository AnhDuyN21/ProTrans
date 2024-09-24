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
    public class AssignmentTranslationConfiguration : IEntityTypeConfiguration<AssignmentTranslation>
    {
        public void Configure(EntityTypeBuilder<AssignmentTranslation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.AssignmentTranslations)
                .HasForeignKey(x => x.TranslatorId);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.AssignmentTranslations)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}

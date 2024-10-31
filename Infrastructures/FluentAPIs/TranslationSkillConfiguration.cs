using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class TranslationSkillConfiguration : IEntityTypeConfiguration<TranslationSkill>
    {
        public void Configure(EntityTypeBuilder<TranslationSkill> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Language)
                .WithMany(x => x.TranslationSkills)
                .HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Translator)
                .WithMany(x => x.TranslationSkills)
                .HasForeignKey(x => x.TranslatorId);
        }
    }
}

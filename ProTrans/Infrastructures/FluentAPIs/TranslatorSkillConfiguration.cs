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
    public class TranslatorSkillConfiguration : IEntityTypeConfiguration<TranslatorSkill>
    {
        public void Configure(EntityTypeBuilder<TranslatorSkill> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Language)
                .WithMany(x => x.TranslatorSkills)
                .HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.TranslatorSkills)
                .HasForeignKey(x => x.TranslatorId);
        }
    }
}

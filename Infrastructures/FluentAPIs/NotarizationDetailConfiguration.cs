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
    public class NotarizationDetailConfiguration : IEntityTypeConfiguration<NotarizationDetail>
    {
        public void Configure(EntityTypeBuilder<NotarizationDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.AssignmentNotarization)
                .WithMany(x => x.NotarizationDetails)
                .HasForeignKey(x => x.AssignmentNotarizationId);

            builder.HasOne(x => x.Document)
                .WithOne(x => x.NotarizationDetails);

        }
    }
}

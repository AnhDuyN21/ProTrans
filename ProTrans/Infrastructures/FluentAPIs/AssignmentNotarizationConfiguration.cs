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
    public class AssignmentNotarizationConfiguration : IEntityTypeConfiguration<AssignmentNotarization>
    {
        public void Configure(EntityTypeBuilder<AssignmentNotarization> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Account)
                .WithMany(x => x.AssignmentNotarizations)
                .HasForeignKey(x => x.StaffId);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.AssignmentNotarizations)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}

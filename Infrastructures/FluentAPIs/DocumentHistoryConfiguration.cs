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
    public class DocumentHistoryConfiguration : IEntityTypeConfiguration<DocumentHistory>
    {
        public void Configure(EntityTypeBuilder<DocumentHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Document)
                .WithMany(x => x.DocumentHistorys)
                .HasForeignKey(x => x.DocumentId);
        }
    }
}

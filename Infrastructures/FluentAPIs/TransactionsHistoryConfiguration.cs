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
    public class TransactionsHistoryConfiguration : IEntityTypeConfiguration<TransactionsHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionsHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.TransactionsHistory)
                .HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.TransactionsHistory)
                .HasForeignKey<TransactionsHistory>(x => x.OrderId);
        }
    }
}

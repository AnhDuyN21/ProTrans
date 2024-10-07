using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
	public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Account)
				.WithMany(x => x.Transactions)
				.HasForeignKey(x => x.AccountId);

			builder.HasOne(x => x.Order)
				.WithOne(x => x.Transaction)
				.HasForeignKey<Transaction>(x => x.OrderId);
		}
	}
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Agency)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.AgencyId);

        }
    }
}

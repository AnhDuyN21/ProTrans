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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.Agency).WithMany(x => x.Accounts).HasForeignKey(x => x.AgencyId);
            builder.HasMany(x => x.Requests).WithOne(x => x.Account);
            builder.HasMany(x => x.TranslatorSkills).WithOne(x => x.Account);
            builder.HasMany(x => x.FeedBacks).WithOne(x => x.Account);
            builder.HasMany(x => x.Images).WithOne(x => x.Account);
            builder.HasMany(x => x.Shippings).WithOne(x => x.Account);
            builder.HasMany(x => x.AssignmentTranslations).WithOne(x => x.Account);
            builder.HasMany(x => x.AssignmentNotarizations).WithOne(x => x.Account);
            builder.HasMany(x => x.TransactionsHistory).WithOne(x => x.Account);
        }
    }
}

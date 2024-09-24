using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Account
{
    public class AccountRepository : GenericRepository<Domain.Entities.Account>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        public AccountRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}

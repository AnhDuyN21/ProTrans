using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public Task<bool> CheckEmailNameExited(string email) =>
                                         _dbContext.Account.AnyAsync(u => u.Email == email);

        public Task<bool> CheckPhoneNumberExited(string phonenumber) =>
                                                _dbContext.Account.AnyAsync(u => u.PhoneNumber == phonenumber);
        public Task<bool> CheckCodeExited(string code) =>
                                                _dbContext.Account.AnyAsync(u => u.Code == code);
    }
}

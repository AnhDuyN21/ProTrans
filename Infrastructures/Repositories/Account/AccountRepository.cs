using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Account;
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
        public Task<Domain.Entities.Account> CheckLogin(string email, string password) =>
            _dbContext.Account.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        public string CheckRoleNameByRoleId(Guid roleId)
        {
            var role = _dbContext.Role.FirstOrDefault(u => u.Id == roleId);
            if (role == null)
            {
                return "Id không tồn tại";
            }
            return role.Name;
        }
    }
}

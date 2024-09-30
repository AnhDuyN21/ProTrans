using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Role;

namespace Infrastructures.Repositories.Role
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;
        public RoleRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _dbContext = context;
            _timeService = timeService;
            _claimsService = claimsService;
        }
        public string GetRoleName(Guid roleId)
        {
            var role = _dbContext.Role.Where(r => r.Id == roleId).FirstOrDefault();
            return role.Name;
        }
        public Guid GetIdCustomerRole()
        {
            var customerRole = _dbContext.Role.FirstOrDefault(r => r.Name == "Customer");
            return customerRole.Id;
        }

    }
}

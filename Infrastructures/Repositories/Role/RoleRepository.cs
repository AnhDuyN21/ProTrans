using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Role;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public Guid GetRoleIdByName(string roleName)
        {
            var customerRole = _dbContext.Role.FirstOrDefault(r => r.Name == roleName);
            return customerRole.Id;
        }

        public async Task<List<Domain.Entities.Role>> GetRoles(Expression<Func<Domain.Entities.Role, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Domain.Entities.Role> query = _dbContext.Role;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return await query.ToListAsync();
        }
      
    }
}

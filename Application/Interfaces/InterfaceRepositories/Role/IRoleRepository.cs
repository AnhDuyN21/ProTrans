using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.Role
{
    public interface IRoleRepository
    {
        string GetRoleName(Guid roleId);
        public Guid GetRoleIdByName(string roleName);
        public Task<List<Domain.Entities.Role>> GetRoles(Expression<Func<Domain.Entities.Role, bool>>? filter = null, string? includeProperties = null);
    }
}

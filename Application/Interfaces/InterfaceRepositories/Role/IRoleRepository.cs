namespace Application.Interfaces.InterfaceRepositories.Role
{
    public interface IRoleRepository
    {
        string GetRoleName(Guid roleId);
        public Guid GetRoleIdByName(string roleName);
    }
}

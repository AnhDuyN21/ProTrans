namespace Application.Interfaces.InterfaceRepositories.Role
{
    public interface IRoleRepository
    {
        string GetRoleName(Guid roleId);
        Guid GetIdCustomerRole();
    }
}

namespace Application.Interfaces.InterfaceRepositories.Account
{
    public interface IAccountRepository : IGenericRepository<Domain.Entities.Account>
    {
        Task<bool> CheckEmailNameExited(string email);
        Task<bool> CheckPhoneNumberExited(string phonenumber);
        Task<bool> CheckCodeExited(string code);
        Task<Domain.Entities.Account> CheckLogin(string email, string password);
        string CheckRoleNameByRoleId(Guid roleId);
    }
}

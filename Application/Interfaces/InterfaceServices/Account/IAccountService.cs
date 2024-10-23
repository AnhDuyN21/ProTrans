using Application.Commons;
using Application.ViewModels.AccountDTOs;

namespace Application.Interfaces.InterfaceServices.Account
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(Guid id);
        Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createAccountDTO);
        Task<ServiceResponse<bool>> DeleteAccountAsync(Guid id);
        Task<ServiceResponse<AccountDTO>> UpdateAccountAsync(Guid id, AccountDTO accountDTO);
        Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterDTO registerDTO);
        Task<ServiceResponse<string>> LoginAsync(LoginDTO loginDTO);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetTranslatorsByLanguageId(Guid id);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountByRoleAsync(Guid id,Guid agencyid);
    }
}

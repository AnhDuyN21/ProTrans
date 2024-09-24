using Application.Commons;
using Application.ViewModels.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Account
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(Guid id);
        Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createAccountDTO);
        Task<ServiceResponse<bool>> DeleteUserAsync(Guid id);
        Task<ServiceResponse<AccountDTO>> UpdateUserAsync(Guid id, AccountDTO accountDTO);
    }
}

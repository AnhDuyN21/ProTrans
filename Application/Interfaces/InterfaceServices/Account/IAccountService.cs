using Application.Commons;
using Application.ViewModels.AccountDTOs;

namespace Application.Interfaces.InterfaceServices.Account
{
	public interface IAccountService
	{
		Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
		Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(Guid id);
		Task<ServiceResponse<AccountDTO>> GetAccountByPhoneNumberAsync(string phoneNumber);
		Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createAccountDTO);
		Task<ServiceResponse<bool>> DeleteAccountAsync(Guid id);
		Task<ServiceResponse<AccountDTO>> UpdateAccountAsync(Guid id, AccountDTO accountDTO);
		Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterDTO registerDTO);
		Task<ServiceResponse<string>> LoginAsync(LoginDTO loginDTO);
		Task<ServiceResponse<IEnumerable<TranslatorAccountDTO>>> GetTranslatorsBy2LanguageId(Guid firstLanguageId, Guid secondLanguageId, Guid documentId);
		Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountByAgencyAsync(Guid agencyid);
		Task<ServiceResponse<IEnumerable<AccountDTO>>> GetShipperAsync();
		Task<ServiceResponse<IEnumerable<AccountDTO>>> GetShipperByAgencyIdAsync(Guid agencyId);
		Task<ServiceResponse<CreateTranslatorDTO>> CreateTranslatorAccountAsync(CreateTranslatorDTO createTranslatorDTO);
		Task<ServiceResponse<IEnumerable<AccountDTO>>> GetStaffAsync();
		Task<ServiceResponse<IEnumerable<TranslatorAccountDTO>>> GetTranslatorAsync();
		Task<ServiceResponse<AccountDTO>> ToggleAccountStatusAsync(Guid id);
		Task<ServiceResponse<bool>> ConfirmEmail(string token);
	}
}

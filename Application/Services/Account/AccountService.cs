using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Account;
using Application.Utils;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTime _currentTime;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _currentTime = currentTime;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync();
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account list retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have account in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetTranslatorsByLanguageId(Guid id)
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();
            var accountList = new List<Domain.Entities.Account>();
            try
            {
                var accountIdList = await _unitOfWork.TranslatorSkillRepository.GetTranslatorIdsByLanguageIdAsync(id);
                if (accountIdList != null)
                {
                    foreach (var accountId in accountIdList)
                    {
                        var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
                        if (account != null) accountList.Add(account);
                    }
                }
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Translator list retrieved successfully.";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "No translator exists.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error.";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public async Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(Guid id)
        {
            var response = new ServiceResponse<AccountDTO>();

            var accountGetById = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (accountGetById == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<AccountDTO>(accountGetById);
            }

            return response;
        }
        public async Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();
            try
            {
                var emailExsit = await _unitOfWork.AccountRepository.CheckEmailNameExited(createAccountDTO.Email);
                if (emailExsit)
                {
                    response.Success = false;
                    response.Message = "Email is existed, Create Fail";
                    return response;
                }
                var phoneExsit = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(createAccountDTO.PhoneNumber);
                if (phoneExsit)
                {
                    response.Success = false;
                    response.Message = "PhoneNumber is existed, Create Fail";
                    return response;
                }
                var account = _mapper.Map<Domain.Entities.Account>(createAccountDTO);
                account.Password = Utils.HashPassword.HashWithSHA256(createAccountDTO.Password);
                account.Code = GenerateRandomCode(createAccountDTO.RoleId);
                await _unitOfWork.AccountRepository.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO;
                    response.Success = true;
                    response.Message = "Account created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the account.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteAccountAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var accountGetById = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (accountGetById == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AccountRepository.SoftRemove(accountGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        public async Task<ServiceResponse<AccountDTO>> UpdateAccountAsync(Guid id, AccountDTO accountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var accountGetById = await _unitOfWork.AccountRepository.GetByIdAsync(id);

                if (accountGetById == null)
                {
                    response.Success = false;
                    response.Message = "Account not found.";
                    return response;
                }
                if ((bool)accountGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Account is deleted in system";
                    return response;
                }

                var objectToUpdate = _mapper.Map(accountDTO, accountGetById);
                objectToUpdate.Password = Utils.HashPassword.HashWithSHA256(accountDTO.Password);

                _unitOfWork.AccountRepository.Update(accountGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AccountDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "Account updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        public async Task<ServiceResponse<string>> LoginAsync(LoginDTO loginDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var hashedPassword = Utils.HashPassword.HashWithSHA256(loginDTO.Password);
                var user = await _unitOfWork.AccountRepository.CheckLogin(loginDTO.Email, hashedPassword);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }
                //if (user.ConfirmToken != null)
                //{
                //    //System.Console.WriteLine(user.ConfirmationToken + user.IsConfirmed);
                //    response.Success = false;
                //    response.Message = "Please confirm via link in your mail";
                //    return response;
                //}
                if (user.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Your account have been deleted!";
                    return response;
                }
                var generate = new GenerateJsonWebTokenString(_unitOfWork);

                var token = generate.GenerateJsonWebToken(user, _configuration, _configuration.GetSection("JWTSection:SecretKey").Value, _currentTime.GetCurrentTime());

                //var token = user.GenerateJsonWebToken(
                //    _configuration,
                //    _configuration.JWTSection.SecretKey,
                //    _currentTime.GetCurrentTime()
                //    );

                response.Success = true;
                response.Message = "Login successfully.";
                response.Data = token;
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        public async Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var response = new ServiceResponse<AccountDTO>();
            try
            {
                var emailExsit = await _unitOfWork.AccountRepository.CheckEmailNameExited(registerDTO.Email);
                if (emailExsit)
                {
                    response.Success = false;
                    response.Message = "Email is existed";
                    return response;
                }
                var phoneExsit = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(registerDTO.PhoneNumber);
                if (phoneExsit)
                {
                    response.Success = false;
                    response.Message = "PhoneNumber is existed";
                    return response;
                }

                var newAccount = _mapper.Map<Domain.Entities.Account>(registerDTO);
                newAccount.Password = Utils.HashPassword.HashWithSHA256(registerDTO.Password);
                //Code
                var roleId = _unitOfWork.RoleRepository.GetRoleIdByName("Customer");
                var codeExist = await _unitOfWork.AccountRepository.CheckCodeExited(GenerateRandomCode(roleId));
                string newCode;
                do
                {
                    newCode = GenerateRandomCode(roleId);
                    codeExist = await _unitOfWork.AccountRepository.CheckCodeExited(newCode);
                }
                while (codeExist);
                newAccount.Code = newCode;
                newAccount.RoleId = _unitOfWork.RoleRepository.GetRoleIdByName("Customer");
                await _unitOfWork.AccountRepository.AddAsync(newAccount);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<AccountDTO>(newAccount);
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Register successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the account.";
                }

            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        public string GenerateRandomCode(Guid roleId)
        {
            Random random = new Random();
            string randomNumber = "";
            for (int i = 0; i < 6; i++)
            {
                randomNumber += random.Next(1, 10);
            }
            string roleName = _unitOfWork.AccountRepository.CheckRoleNameByRoleId(roleId);
            if (roleName.Equals("Customer"))
            {
                return "CU" + randomNumber;
            }
            else if (roleName.Equals("Translator"))
            {
                return "TR" + randomNumber;
            }
            else if (roleName.Equals("Staff"))
            {
                return "ST" + randomNumber;
            }
            else if (roleName.Equals("Manager"))
            {
                return "MA" + randomNumber;
            }
            else if (roleName.Equals("Admin"))
            {
                return "AD" + randomNumber;
            }
            else if (roleName.Equals("Shipper"))
            {
                return "SH" + randomNumber;
            }
            return "không thể tạo code";
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountByAgencyAsync(Guid agencyId)
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync( x=>x.AgencyId.Equals(agencyId));
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account list retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have account in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetShipperAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role.Name.Equals("Shipper"));
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account list retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have account in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
        public async Task<ServiceResponse<CreateTranslatorDTO>> CreateTranslatorAccountAsync(CreateTranslatorDTO createTranslatorDTO)
        {
            var response = new ServiceResponse<CreateTranslatorDTO>();
            try
            {
                var emailExsit = await _unitOfWork.AccountRepository.CheckEmailNameExited(createTranslatorDTO.Email);
                if (emailExsit)
                {
                    response.Success = false;
                    response.Message = "Email is existed, Create Fail";
                    return response;
                }
                var phoneExsit = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(createTranslatorDTO.PhoneNumber);
                if (phoneExsit)
                {
                    response.Success = false;
                    response.Message = "PhoneNumber is existed, Create Fail";
                    return response;
                }
                var newAccount = _mapper.Map<Domain.Entities.Account>(createTranslatorDTO);
                newAccount.Password = Utils.HashPassword.HashWithSHA256(createTranslatorDTO.Password);
                //Code
                var roleId = _unitOfWork.RoleRepository.GetRoleIdByName("Translator");
                var codeExist = await _unitOfWork.AccountRepository.CheckCodeExited(GenerateRandomCode(roleId));
                string newCode;
                do
                {
                    newCode = GenerateRandomCode(roleId);
                    codeExist = await _unitOfWork.AccountRepository.CheckCodeExited(newCode);
                }
                while (codeExist);
                newAccount.Code = newCode;
                newAccount.RoleId = _unitOfWork.RoleRepository.GetRoleIdByName("Translator");
                //Skills
                if(createTranslatorDTO.Skills != null)
                {
                    foreach(var skill in createTranslatorDTO.Skills)
                    {
                        var mappedObject = _mapper.Map<Domain.Entities.TranslationSkill>(skill);
                        mappedObject.TranslatorId = newAccount.Id;
                        await _unitOfWork.TranslatorSkillRepository.AddAsync(mappedObject);
                    }
                }
                await _unitOfWork.AccountRepository.AddAsync(newAccount);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Success !";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the account.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetTranslatorAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role.Name.Equals("Translator"));
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account list retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have account in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetStaffAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accountList = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role.Name.Equals("Staff"));
                var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account list retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have account in list";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }
    }
}

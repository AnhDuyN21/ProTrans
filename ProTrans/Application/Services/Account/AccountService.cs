using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Account;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            //var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(createdAccountDTO.Email);
            //var existed = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(createdAccountDTO.PhoneNumber);

            //if (exist)
            //{
            //    response.Success = false;
            //    response.Message = "Email is existed";
            //    return response;
            //}
            //else if (existed)
            //{
            //    response.Success = false;
            //    response.Message = "Phone is existed";
            //    return response;
            //}
            try
            {
                var account = _mapper.Map<Domain.Entities.Account>(createAccountDTO);
                account.Password = Utils.HashPassword.HashWithSHA256(createAccountDTO.Password);

                await _unitOfWork.AccountRepository.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                    response.Success = true;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
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
        public async Task<ServiceResponse<bool>> DeleteUserAsync(Guid id)
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
        public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(Guid id, AccountDTO accountDTO)
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
                // Map accountDT0 => existingUser
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

    }
}

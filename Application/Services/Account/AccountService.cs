﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Account;
using Application.Utils;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.SendMail;
using Application.ViewModels.TranslatorSkillDTOs;
using AutoMapper;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Net.WebSockets;

namespace Application.Services.Account
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly ICurrentTime _currentTime;
		private readonly ISendMail _sendMail;
		public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ICurrentTime currentTime, ISendMail sendMail)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_configuration = configuration;
			_currentTime = currentTime;
			_sendMail = sendMail;
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

		public async Task<ServiceResponse<IEnumerable<TranslatorAccountDTO>>> GetTranslatorsBy2LanguageId(Guid firstLanguageId, Guid secondLanguageId, Guid documentId)
		{
			var response = new ServiceResponse<IEnumerable<TranslatorAccountDTO>>();
			var accountList = new List<Domain.Entities.Account>();

			try
			{
				Guid agencyId = Guid.Empty;
				var document = await _unitOfWork.DocumentRepository.GetByIdAsync(documentId);
				if (document != null && document.OrderId != null)
				{
					var order = await _unitOfWork.OrderRepository.GetByIdAsync(document.OrderId.Value);
					if (order != null && order.AgencyId != null)
					{
						agencyId = order.AgencyId.Value;
					}
				}

				var accountDTOs = new List<TranslatorAccountDTO>();
				Guid id = Guid.Empty;
				var firstLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync(firstLanguageId);
				var secondLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync(secondLanguageId);
				if (firstLanguage != null && secondLanguage != null)
				{
					if (firstLanguage.Name == "Việt Nam") id = secondLanguage.Id;
					if (secondLanguage.Name == "Việt Nam") id = firstLanguage.Id;
				}
				var accountIdList = await _unitOfWork.TranslatorSkillRepository.GetTranslatorIdListByLanguageIdAsync(id);
				if (accountIdList != null)
				{
					foreach (var accountId in accountIdList)
					{
						var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
						if (account != null) accountList.Add(account);
						accountDTOs.Add(_mapper.Map<TranslatorAccountDTO>(account));
						if (account != null && account.AgencyId != null && agencyId != Guid.Empty)
						{
							var agency = await _unitOfWork.AgencyRepository.GetByIdAsync(account.AgencyId.Value);
							if (agency != null) accountDTOs.Last().AgencyName = agency.Name;
							var distanceList = await _unitOfWork.DistanceRepository.GetAllAsync(x => x.RootAgencyId == agencyId && x.TargetAgencyId == account.AgencyId);
							decimal value = 0;
							Domain.Entities.Distance distance = null;
							if (distanceList != null && distanceList.Any())
							{
								distance = distanceList[0];
								if (distance.Value != null) value = distance.Value.Value;
							}
							accountDTOs.Last().Distance = value;
							var assignments = await _unitOfWork.AssignmentTranslationRepository.GetAllAsync
								(x => x.TranslatorId == accountId
								&& x.Status == AssignmentTranslationStatus.Translating.ToString());
							accountDTOs.Last().AssignmentsInProgress = assignments.Count;
						}
					}
				}
				//var accountDTOs = _mapper.Map<List<TranslatorAccountDTO>>(accountList);

				if (accountDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Translator list retrieved successfully.";
					response.Data = accountDTOs.OrderBy(account => account.Distance)
														.ThenBy(account => account.AssignmentsInProgress)
																.ToList(); ;
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
		public async Task<ServiceResponse<AccountDTO>> GetAccountByPhoneNumberAsync(string phoneNumber)
		{
			var response = new ServiceResponse<AccountDTO>();

			var accountGetByPhoneNumber = await _unitOfWork.AccountRepository.GetAsync(x => x.PhoneNumber == phoneNumber);
			if (accountGetByPhoneNumber == null)
			{
				response.Success = false;
				response.Message = "Không tìm thấy số điện thoại";
			}
			else
			{
				response.Success = true;
				response.Message = "Account found";
				response.Data = _mapper.Map<AccountDTO>(accountGetByPhoneNumber);
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

		public async Task<ServiceResponse<AccountDTO>> ToggleAccountStatusAsync(Guid id)
		{
			var response = new ServiceResponse<AccountDTO>();
			try
			{
				var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);

				if (account == null)
				{
					response.Success = false;
					response.Message = "Account not found.";
					return response;
				}

				if (account.IsDeleted != null)
				{
					if (account.IsDeleted == true) account.IsDeleted = false;
					else account.IsDeleted = true;
				}

				_unitOfWork.AccountRepository.Update(account);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<AccountDTO>(account);
					response.Success = true;
					response.Message = "Account updated successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error updating account.";
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
					response.Message = "Email hoặc mật khẩu không hợp lệ.";
					return response;
				}
				if (user.ConfirmToken != null)
				{

					response.Success = false;
					response.Message = "Chưa xác thực email";
					return response;
				}

				if (user.IsDeleted == true)
				{
					response.Success = false;
					response.Message = "Tài khoản của bạn đã bị xóa khỏi hệ thống.";
					return response;
				}
				var generate = new GenerateJsonWebTokenString(_unitOfWork);

				var token = generate.GenerateJsonWebToken(user, _configuration, _configuration.GetSection("JWTSection:SecretKey").Value, _currentTime.GetCurrentTime());

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
				//Link xác nhận
				newAccount.ConfirmToken = Guid.NewGuid().ToString();
				var confirmationLink = $"https://protrans.azurewebsites.net/api/Account/confirm?token={newAccount.ConfirmToken}";
				MessageDTO maildto = new MessageDTO
				{
					To = newAccount.Email,
					Subject = "Xác nhận địa chỉ email của bạn",
					Body = $@"
                            <div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                            <h2>Kính gửi {newAccount.FullName},</h2>
                            <p>Cảm ơn bạn đã đăng ký tài khoản tại <strong>ProTrans</strong>.</p>
                            <p>Để hoàn tất quá trình đăng ký và kích hoạt tài khoản, vui lòng xác nhận địa chỉ email của bạn bằng cách nhấn vào liên kết dưới đây:</p>
                            <p style='text-align: center; margin: 20px 0;'>
                            <a href='{confirmationLink}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px; display: inline-block;'>
                                Xác nhận Email
                            </a>
                            </p>
                            <p>Trân trọng,<br>
                            <strong>Đội ngũ ProTrans</strong></p>
                            </div>",
					ImageUrl = "https://imgs.search.brave.com/axX40gDmB0NjE8PrUmglrN-35QWwWubDcXkwPKQSsnI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDgz/NTI3MjM5L3Bob3Rv/L3RoYW5rLXlvdS10/ZXh0LW9uLWhhbmQu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PThSdGtXYTRmRXdx/OUJtUVcycTh0X3FS/amhWdXFOTmh3YWJL/bnZraGJaM0k9"
				};
				await _sendMail.SendEmailAsync(maildto);


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
				var accountList = await _unitOfWork.AccountRepository.GetAllAsync(x => x.AgencyId.Equals(agencyId));
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
		public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetShipperByAgencyIdAsync(Guid agencyId)
		{
			var response = new ServiceResponse<IEnumerable<AccountDTO>>();
			var agency = await _unitOfWork.AgencyRepository.GetByIdAsync(agencyId);
			string agencyName = null;
			if (agency != null)
			{
				agencyName = agency.Name;
			}

			try
			{
				var accountList = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role.Name.Equals("Shipper") && x.AgencyId == agencyId);
				var accountDTOs = _mapper.Map<List<AccountDTO>>(accountList);
				if (agencyName != null) foreach (var accountDTO in accountDTOs)
					{
						accountDTO.AgencyName = agencyName;
					}

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
				if (createTranslatorDTO.Skills.Count == 0)
				{
					response.Success = false;
					response.Message = "Nhân viên dịch phải có ít nhất 1 kĩ năng";
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


				foreach (var skill in createTranslatorDTO.Skills)
				{
					var mappedObject = _mapper.Map<Domain.Entities.TranslationSkill>(skill);
					mappedObject.TranslatorId = newAccount.Id;
					await _unitOfWork.TranslatorSkillRepository.AddAsync(mappedObject);
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
		public async Task<ServiceResponse<IEnumerable<TranslatorAccountDTO>>> GetTranslatorAsync()
		{
			var response = new ServiceResponse<IEnumerable<TranslatorAccountDTO>>();

			try
			{
				var accounts = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role.Name.Equals("Translator"));
				var accountDTOs = _mapper.Map<List<TranslatorAccountDTO>>(accounts).OrderBy(x => x.FullName).ToList();
				foreach (var acc in accountDTOs)
				{
					var skills = await _unitOfWork.TranslatorSkillRepository.GetAllAsync(x => x.TranslatorId == acc.Id);
					if (skills.Count > 0)
					{
						acc.TranslatorSkills = _mapper.Map<List<TranslatorSkillDTO>>(skills);
						foreach (var skill in acc.TranslatorSkills)
						{
							var language = await _unitOfWork.LanguageRepository.GetAsync(x => x.Id == skill.LanguageId);
							if (language != null)
							{
								skill.LanguageName = language.Name;
							}
						}
					}
				}

				if (accountDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Account list retrieved successfully.";
					response.Data = accountDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Fail.";
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
		public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAllRoleAsync()
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
		public async Task<ServiceResponse<bool>> ConfirmEmail(string token)
		{
			var response = new ServiceResponse<bool>();

			try
			{
				var account = await _unitOfWork.AccountRepository.GetAsync(x => x.ConfirmToken == token);
				if (account == null)
				{
					response.Success = false;
					response.Message = "Không tìm thấy account có confirm token";
					return response;
				}
				account.IsConfirm = true;
				account.ConfirmToken = null;
				_unitOfWork.AccountRepository.Update(account);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Xác nhận thành công vui lòng quay lại hệ thống để sử dụng dịch vụ!";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving the account.";
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

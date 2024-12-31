using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.TranslatorSkill;
using Application.ViewModels.TranslatorSkillDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.TranslatorSkill
{
	public class TranslatorSkillService : ITranslatorSkillService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public TranslatorSkillService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<ServiceResponse<TranslatorSkillDTO>> CreateTranslatorSkillAsync(CUTranslatorSkillDTO cuTranslatorSkillDTO)
		{
			var response = new ServiceResponse<TranslatorSkillDTO>();
			try
			{
				var translatorSkill = _mapper.Map<Domain.Entities.TranslationSkill>(cuTranslatorSkillDTO);


				await _unitOfWork.TranslatorSkillRepository.AddAsync(translatorSkill);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var TranslatorSkillDTO = _mapper.Map<TranslatorSkillDTO>(translatorSkill);
					response.Data = TranslatorSkillDTO; // Chuyển đổi sang TranslatorSkillDTO
					response.Success = true;
					response.Message = "Translator skill created successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving the skill.";
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

		public async Task<ServiceResponse<bool>> DeleteTranslatorSkillAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var translatorSkillGetById = await _unitOfWork.TranslatorSkillRepository.GetByIdAsync(id);
			if (translatorSkillGetById == null)
			{
				response.Success = false;
				response.Message = "TranslatorSkill is not existed";
				return response;
			}

			try
			{
				_unitOfWork.TranslatorSkillRepository.SoftRemove(translatorSkillGetById);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "TranslatorSkill deleted successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error deleting the TranslatorSkill.";
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

		public async Task<ServiceResponse<IEnumerable<TranslatorSkillDTO>>> GetAllTranslatorSkillsAsync()
		{
			var response = new ServiceResponse<IEnumerable<TranslatorSkillDTO>>();

			try
			{
				var translatorSkillById = await _unitOfWork.TranslatorSkillRepository.GetAllAsync(x => x.IsDeleted == false);
				var translatorSkillDTOs = _mapper.Map<List<TranslatorSkillDTO>>(translatorSkillById.Select(ts => new TranslatorSkillDTO
				{
					Id = ts.Id,
					TranslatorId = ts.TranslatorId,
					LanguageId = ts.LanguageId,
					CertificateUrl = ts.CertificateUrl
				}));

				if (translatorSkillById == null)
				{
					response.Success = false;
					response.Message = "Translator skills not found";

				}
				else
				{
					response.Success = true;
					response.Message = "Translator skill retrieved successfully ";
					response.Data = translatorSkillDTOs;
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

		public async Task<ServiceResponse<TranslatorSkillDTO>> GetTranslatorSkillByIdAsync(Guid Id)
		{
			var response = new ServiceResponse<TranslatorSkillDTO>();

			try
			{
				var translatorSkillById = await _unitOfWork.TranslatorSkillRepository.GetTranslatorSkillByIdAsync(x => x.TranslatorId.Equals(Id) && x.IsDeleted == false);
				var translatorSkillDTOs = _mapper.Map<TranslatorSkillDTO>(translatorSkillById);
				translatorSkillDTOs.TranslatorId = translatorSkillById.TranslatorId;
				translatorSkillDTOs.LanguageId = translatorSkillById.LanguageId;

				if (translatorSkillById == null)
				{
					response.Success = false;
					response.Message = "Translator skills not found";

				}
				else
				{
					response.Success = true;
					response.Message = "Translator skill retrieved successfully";
					response.Data = translatorSkillDTOs;
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

		public async Task<ServiceResponse<TranslatorSkillDTO>> UpdateTranslatorSkillAsync(Guid id, CUTranslatorSkillDTO cuTranslatorSkillDTO)
		{
			var response = new ServiceResponse<TranslatorSkillDTO>();

			try
			{
				var TranslatorSkillGetById = await _unitOfWork.TranslatorSkillRepository.GetByIdAsync(id);

				if (TranslatorSkillGetById == null)
				{
					response.Success = false;
					response.Message = "TranslatorSkill not found.";
					return response;
				}
				if ((bool)TranslatorSkillGetById.IsDeleted)
				{
					response.Success = false;
					response.Message = "TranslatorSkill is deleted in system";
					return response;
				}
				// Map TranslatorSkillDT0 => existingUser
				var objectToUpdate = _mapper.Map(cuTranslatorSkillDTO, TranslatorSkillGetById);


				_unitOfWork.TranslatorSkillRepository.Update(TranslatorSkillGetById);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<TranslatorSkillDTO>(objectToUpdate);
					response.Success = true;
					response.Message = "TranslatorSkill updated successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error updating the TranslatorSkill.";
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
		public async Task<ServiceResponse<IEnumerable<TranslatorSkillDTO>>> GetTranslatorSkillByLanguageAsync(Guid Id)
		{
			var response = new ServiceResponse<IEnumerable<TranslatorSkillDTO>>();

			try
			{
				var translatorSkillById = await _unitOfWork.TranslatorSkillRepository.GetAllAsync(x => x.LanguageId.Equals(Id) && x.IsDeleted == false);
				var translatorSkillDTOs = _mapper.Map<List<TranslatorSkillDTO>>(translatorSkillById.Select(ts => new TranslatorSkillDTO
				{
					Id = ts.Id,
					TranslatorId = ts.TranslatorId,
					LanguageId = ts.LanguageId,
					CertificateUrl = ts.CertificateUrl
				}));

				if (translatorSkillById == null)
				{
					response.Success = false;
					response.Message = "Translator skills not found";

				}
				else
				{
					response.Success = true;
					response.Message = "Translator skill retrieved successfully ";
					response.Data = translatorSkillDTOs;
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

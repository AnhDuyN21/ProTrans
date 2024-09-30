using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Language;
using Application.ViewModels.LanguageDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<LanguageDTO>> CreateLanguageAsync(CULanguageDTO cudLanguageDTO)
        {
            var response = new ServiceResponse<LanguageDTO>();
            try
            {
                var language = _mapper.Map<Domain.Entities.Language>(cudLanguageDTO);


                await _unitOfWork.LanguageRepository.AddAsync(language);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var languageDTO = _mapper.Map<LanguageDTO>(language);
                    response.Data = languageDTO; // Chuyển đổi sang languageDTO
                    response.Success = true;
                    response.Message = "Tạo ngôn ngữ thành công.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Lỗi lưu ngôn ngữ.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Cơ sỡ dữ liệu xảy ra lỗi.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Lỗi";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteLanguageAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var LanguageGetById = await _unitOfWork.LanguageRepository.GetByIdAsync(id);
            if (LanguageGetById == null)
            {
                response.Success = false;
                response.Message = "Xóa thất bại, không tìm thấy dữ liệu";
                return response;
            }

            try
            {
                _unitOfWork.LanguageRepository.SoftRemove(LanguageGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Xóa ngôn ngữ thành công";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Lỗi khi xóa ngôn ngữ.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Lỗi";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<LanguageDTO>>> GetAllLanguagesAsync()
        {
            var response = new ServiceResponse<IEnumerable<LanguageDTO>>();

            try
            {
                var languageList = await _unitOfWork.LanguageRepository.GetAllAsync();
                var languageDTOs = _mapper.Map<List<LanguageDTO>>(languageList);

                if (languageDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Tìm kiếm ngôn ngữ thành công";
                    response.Data = languageDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Không tồn tại ngôn ngữ nào";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Lỗi";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<ServiceResponse<LanguageDTO>> GetLanguageByIdAsync(Guid Id)
        {
            var response = new ServiceResponse<LanguageDTO>();

            var languageGetById = await _unitOfWork.LanguageRepository.GetByIdAsync(Id);
            if (languageGetById == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<LanguageDTO>(languageGetById);
            }

            return response;
        }

        public async Task<ServiceResponse<LanguageDTO>> UpdateLanguageAsync(Guid id, CULanguageDTO cudLanguageDTO)
        {
            var response = new ServiceResponse<LanguageDTO>();

            try
            {
                var languageGetById = await _unitOfWork.LanguageRepository.GetByIdAsync(id);

                if (languageGetById == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy ngôn ngữ.";
                    return response;
                }
                if ((bool)languageGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Ngôn ngữ đã bị xóa";
                    return response;
                }
                // Map LanguageDT0 => existingUser
                var objectToUpdate = _mapper.Map(cudLanguageDTO, languageGetById);


                _unitOfWork.LanguageRepository.Update(languageGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<LanguageDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "Cập nhật ngôn ngữ thành công.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Lỗi khi đang cập nhật ngôn ngữ.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Lỗi";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}

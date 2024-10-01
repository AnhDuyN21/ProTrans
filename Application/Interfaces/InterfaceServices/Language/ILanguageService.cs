using Application.Commons;
using Application.ViewModels.LanguageDTOs;

namespace Application.Interfaces.InterfaceServices.Language
{
    public interface ILanguageService
    {
        public Task<ServiceResponse<IEnumerable<LanguageDTO>>> GetAllLanguagesAsync();
        public Task<ServiceResponse<LanguageDTO>> GetLanguageByIdAsync(Guid Id);
        public Task<ServiceResponse<LanguageDTO>> UpdateLanguageAsync(Guid id, CULanguageDTO cudLanguageDTO);
        public Task<ServiceResponse<LanguageDTO>> CreateLanguageAsync(CULanguageDTO languageDTO);
        public Task<ServiceResponse<bool>> DeleteLanguageAsync(Guid id);

    }
}

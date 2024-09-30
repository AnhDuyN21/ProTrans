using Application.Commons;
using Application.ViewModels.TranslatorSkillDTOs;

namespace Application.Interfaces.InterfaceServices.TranslatorSkill
{
    public interface ITranslatorSkillService
    {
        public Task<ServiceResponse<IEnumerable<TranslatorSkillDTO>>> GetAllTranslatorSkillsAsync();
        public Task<ServiceResponse<TranslatorSkillDTO>> GetTranslatorSkillByIdAsync(Guid Id);
        public Task<ServiceResponse<TranslatorSkillDTO>> UpdateTranslatorSkillAsync(Guid id, CUTranslatorSkillDTO cuTranslatorSkillDTO);
        public Task<ServiceResponse<TranslatorSkillDTO>> CreateTranslatorSkillAsync(CUTranslatorSkillDTO cuTranslatorSkillDTO);
        public Task<ServiceResponse<bool>> DeleteTranslatorSkillAsync(Guid id);

    }
}

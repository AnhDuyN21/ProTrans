using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.TranslationSkill
{
    public interface ITranslatorSkillRepository : IGenericRepository<Domain.Entities.TranslationSkill>
    {
        Task<List<Domain.Entities.TranslationSkill>> GetAllTranslatorSkillAsync(Expression<Func<Domain.Entities.TranslationSkill, bool>>? filter = null, string? includeProperties = null);
        Task<Domain.Entities.TranslationSkill> GetTranslatorSkillByIdAsync(Expression<Func<Domain.Entities.TranslationSkill, bool>>? filter = null, string? includeProperties = null);
        Task<List<Guid>> GetTranslatorIdsByLanguageIdAsync(Guid id);
    }
}

using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.TranslatorSkill
{
    public interface ITranslatorSkillRepository : IGenericRepository<Domain.Entities.TranslatorSkill>
    {
        Task<List<Domain.Entities.TranslatorSkill>> GetAllTranslatorSkillAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null);
        Task<Domain.Entities.TranslatorSkill> GetTranslatorSkillByIdAsync(Guid id);
    }
}

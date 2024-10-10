using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.AssignmentTranslation
{
    public interface IAssignmentTranslationRepository : IGenericRepository<Domain.Entities.AssignmentTranslation>
    {
        Task<List<Domain.Entities.AssignmentTranslation>> GetAllAssimentTranslationByTranslatorIdAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null);
    }
}

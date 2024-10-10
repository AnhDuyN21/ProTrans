using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.AssignmentNotarization
{
    public interface IAssignmentNotarizationRepository : IGenericRepository<Domain.Entities.AssignmentNotarization>
    {
        Task<List<Domain.Entities.AssignmentNotarization>> GetAllAssignmentNotarizationAsync(Expression<Func<Domain.Entities.AssignmentNotarization, bool>>? filter = null, string? includeProperties = null);
    }
}

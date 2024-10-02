using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.AssignmentNotarization
{
    public interface IAssignmentNotarizationRepository : IGenericRepository<Domain.Entities.AssignmentNotarization>
    {
        Task<List<Domain.Entities.AssignmentNotarization>> GetAllAssignmentNotarizationAsync(Expression<Func<Domain.Entities.AssignmentNotarization, bool>>? filter = null, string? includeProperties = null);
    }
}

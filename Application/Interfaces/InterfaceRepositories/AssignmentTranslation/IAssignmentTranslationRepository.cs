using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.AssignmentTranslation
{
    public interface IAssignmentTranslationRepository : IGenericRepository<Domain.Entities.AssignmentTranslation>
    {
        Task<List<Domain.Entities.AssignmentTranslation>> GetAllAssimentTranslationByTranslatorIdAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null);
    }
}

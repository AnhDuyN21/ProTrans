using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.AssignmentTranslation
{
    public class AssignmentTranslationRepository : GenericRepository<Domain.Entities.AssignmentTranslation>, IAssignmentTranslationRepository
    {
        private readonly AppDbContext _dbContext;
        public AssignmentTranslationRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Domain.Entities.AssignmentTranslation>> GetAllAssimentTranslationByTranslatorIdAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Domain.Entities.AssignmentTranslation> query = _dbSet;
            //.Select(q => new Domain.Entities.AssignmentTranslation
            //{
            //    Id = q.Id, // Assuming you want Id in the final result
            //    TranslatorId = q.TranslatorId,
            //})
            //.AsQueryable();

            //    if (filter != null)
            //    {
            //        query = query.Where(filter);
            //    }

            //    if (includeProperties != null)
            //    {
            //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //        {
            //            query = query.Include(includeProp);

            //        }
            //    }
            return await query.ToListAsync();
        }
    }
}

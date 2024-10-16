using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.AssignmentNotarization
{
    public class AssignmentNotarizationRepository : GenericRepository<Domain.Entities.AssignmentNotarization>, IAssignmentNotarizationRepository
    {
        private readonly AppDbContext _dbContext;
        public AssignmentNotarizationRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Domain.Entities.AssignmentNotarization>> GetAllAssignmentNotarizationAsync(Expression<Func<Domain.Entities.AssignmentNotarization, bool>>? filter = null, string? includeProperties = null)
        {

            IQueryable<Domain.Entities.AssignmentNotarization> query = _dbSet
        .Select(q => new Domain.Entities.AssignmentNotarization
        {
            Id = q.Id, // Assuming you want Id in the final result
            ShipperId = q.ShipperId,
            Document = q.Document,
            NumberOfNotarization = q.NumberOfNotarization,
            Status = q.Status,
            IsDeleted = q.IsDeleted,
        })
        .AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return await query.ToListAsync();
        }
    }
}

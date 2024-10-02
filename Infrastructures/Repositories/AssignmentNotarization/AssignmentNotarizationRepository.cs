using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

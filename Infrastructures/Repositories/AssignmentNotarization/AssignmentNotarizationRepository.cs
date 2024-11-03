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
    }
}

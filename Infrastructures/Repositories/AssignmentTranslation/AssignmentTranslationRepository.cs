using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;

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
    }
}

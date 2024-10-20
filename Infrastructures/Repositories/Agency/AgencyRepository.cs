using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Agency;

namespace Infrastructures.Repositories.Agency
{
    public class AgencyRepository : GenericRepository<Domain.Entities.Agency>, IAgencyRepository
    {
        private readonly AppDbContext _dbContext;
        public AgencyRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}

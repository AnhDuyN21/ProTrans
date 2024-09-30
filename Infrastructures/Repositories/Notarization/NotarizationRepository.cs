using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Notarization;

namespace Infrastructures.Repositories.Notarization
{
    public class NotarizationRepository : GenericRepository<Domain.Entities.Notarization>, INotarizationRepository
    {
        private readonly AppDbContext _dbContext;
        public NotarizationRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}

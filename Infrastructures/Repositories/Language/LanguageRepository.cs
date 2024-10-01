using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Language;

namespace Infrastructures.Repositories.Language
{
    public class LanguageRepository : GenericRepository<Domain.Entities.Language>, ILanguageRepository
    {
        private readonly AppDbContext _dbContext;
        public LanguageRepository(
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

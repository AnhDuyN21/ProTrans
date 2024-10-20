using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories.Account
{
    public class QuotePriceRepository : GenericRepository<Domain.Entities.QuotePrice>, IQuotePriceRepository
    {
        private readonly AppDbContext _dbContext;
        public QuotePriceRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<QuotePrice> GetQuotePriceBy2LanguageId(Guid id1, Guid id2)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.FirstLanguageId == id1 & x.SecondLanguageId == id2);
            return result;
        }
    }
}
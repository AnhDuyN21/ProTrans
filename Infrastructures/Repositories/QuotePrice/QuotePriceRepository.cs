using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<QuotePrice>> GetAllQuotePriceAsync(Expression<Func<QuotePrice, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<QuotePrice> query = _dbSet
        .Select(q => new QuotePrice
        {
            Id = q.Id, // Assuming you want Id in the final result
            PricePerPage = q.PricePerPage,
            FirstLanguage = new Domain.Entities.Language { Name = q.FirstLanguage.Name }, // Use constructor with Name property
            SecondLanguage = new Domain.Entities.Language { Name = q.SecondLanguage.Name } // Use constructor with Name property
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



        public async Task<QuotePrice> GetQuotePriceByIdAsync(Guid id)
        {

            var result = await _dbSet.
                Select(q => new QuotePrice
                {
                    Id = q.Id, // Assuming you want Id in the final result
                    PricePerPage = q.PricePerPage,
                    FirstLanguage = new Domain.Entities.Language { Name = q.FirstLanguage.Name }, // Use constructor with Name property
                    SecondLanguage = new Domain.Entities.Language { Name = q.SecondLanguage.Name } // Use constructor with Name property
                })
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}

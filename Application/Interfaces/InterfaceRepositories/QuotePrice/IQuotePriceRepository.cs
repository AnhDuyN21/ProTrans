using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.QuotePrice
{
    public interface IQuotePriceRepository : IGenericRepository<Domain.Entities.QuotePrice>
    {
        Task<List<Domain.Entities.QuotePrice>> GetAllQuotePriceAsync(Expression<Func<Domain.Entities.QuotePrice, bool>>? filter = null, string? includeProperties = null);
        Task<Domain.Entities.QuotePrice> GetQuotePriceByIdAsync(Guid id);
        Task<Domain.Entities.QuotePrice> GetQuotePriceBy2LanguageId(Guid id1, Guid id2);

	}
}

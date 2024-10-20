namespace Application.Interfaces.InterfaceRepositories.QuotePrice
{
    public interface IQuotePriceRepository : IGenericRepository<Domain.Entities.QuotePrice>
    {
        Task<Domain.Entities.QuotePrice> GetQuotePriceBy2LanguageId(Guid id1, Guid id2);
    }
}

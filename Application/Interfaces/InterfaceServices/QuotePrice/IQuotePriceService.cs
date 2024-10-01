using Application.Commons;
using Application.ViewModels.QuotePriceDTOs;

namespace Application.Interfaces.InterfaceServices.QuotePrice
{
    public interface IQuotePriceService
    {
        public Task<ServiceResponse<IEnumerable<QuotePriceDTO>>> GetAllQuotePricesAsync();
        public Task<ServiceResponse<QuotePriceDTO>> GetQuotePriceByIdAsync(Guid Id);
        public Task<ServiceResponse<QuotePriceDTO>> UpdateQuotePriceAsync(Guid id, CUQuotePriceDTO cuQuotePriceDTO);
        public Task<ServiceResponse<QuotePriceDTO>> CreateQuotePriceAsync(CUQuotePriceDTO cuQuotePriceDTO);
        public Task<ServiceResponse<bool>> DeleteQuotePriceAsync(Guid id);

    }
}

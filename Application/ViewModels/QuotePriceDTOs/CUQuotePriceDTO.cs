namespace Application.ViewModels.QuotePriceDTOs
{
    public class CUQuotePriceDTO
    {
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public decimal? PricePerPage { get; set; }
    }
}

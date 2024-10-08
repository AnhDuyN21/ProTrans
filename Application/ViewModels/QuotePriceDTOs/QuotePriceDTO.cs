namespace Application.ViewModels.QuotePriceDTOs
{
    public class QuotePriceDTO
    {
        public Guid Id { get; set; }
        public Guid FirstLanguageId { get; set; }
        public Guid SecondLanguageId { get; set; }
        public decimal? PricePerPage { get; set; }
    }
}

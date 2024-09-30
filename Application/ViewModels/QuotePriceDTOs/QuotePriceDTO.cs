namespace Application.ViewModels.QuotePriceDTOs
{
    public class QuotePriceDTO
    {
        public Guid Id { get; set; }
        public required string FirstLanguage { get; set; }
        public required string SecondLanguage { get; set; }
        public decimal? PricePerPage { get; set; }
    }
}

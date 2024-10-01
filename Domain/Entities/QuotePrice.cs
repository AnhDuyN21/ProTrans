namespace Domain.Entities
{
    public class QuotePrice : BaseEntity
    {
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Language? FirstLanguage { get; set; }
        public Language? SecondLanguage { get; set; }
        public decimal? PricePerPage { get; set; }
    }
}

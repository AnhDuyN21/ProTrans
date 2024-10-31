namespace Domain.Entities
{
    public class QuotePrice : BaseEntity
    {
        //Field
        public decimal? PricePerPage { get; set; }
        //Foreignkey
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        //Relationship
        public Language? FirstLanguage { get; set; }
        public Language? SecondLanguage { get; set; }

    }
}

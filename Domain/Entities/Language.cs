namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        //Field
        public string Name { get; set; }
        //Relationship
        public ICollection<QuotePrice>? FirstLanguage_QuotePrice { get; set; }
        public ICollection<QuotePrice>? SecondLanguage_QuotePrice { get; set; }
        public virtual ICollection<TranslationSkill>? TranslationSkills { get; set; }
        public virtual ICollection<Document>? FirstLanguage_Document { get; set; }
        public virtual ICollection<Document>? SecondLanguage_Document { get; set; }
    }
}

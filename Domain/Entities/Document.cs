namespace Domain.Entities
{
    public class Document : BaseEntity
    {
        //Field
        public string? Code { get; set; }
        public string? UrlPath { get; set; }
        public string? FileType { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfCopies { get; set; } 
        public bool NotarizationRequest { get; set; }
        public int NumberOfNotarizedCopies { get; set; }
        public string? TranslationStatus { get; set; }
        public string? NotarizationStatus { get; set; }
        //Foreignkey
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? RequestId { get; set; }
        //Relationship
        public virtual Order? Order { get; set; }
        public virtual Request? Request { get; set; }
        public virtual Notarization? Notarization { get; set; }
        public virtual DocumentType? DocumentType { get; set; }
        public virtual ICollection<AssignmentTranslation>? AssignmentTranslations { get; set; }
        public virtual NotarizationDetail? NotarizationDetails { get; set; }
        public Language? FirstLanguage { get; set; }
        public Language? SecondLanguage { get; set; }
    }
}

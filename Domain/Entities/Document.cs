namespace Domain.Entities
{
    public class Document : BaseEntity
    {
        public Guid? FirstLanguageId { get; set; }
        public Language? FirstLanguage { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Language? SecondLanguage { get; set; }
        public string? Code { get; set; }
        public string? UrlPath { get; set; }
        public string? FileType { get; set; }
        public int PageNumber { get; set; } = 0;
        public int NumberOfCopies { get; set; } = 0;
        public bool NotarizationRequest { get; set; } = false;
        public int NumberOfNotarizedCopies { get; set; } = 0;
        public string? TranslationStatus { get; set; }
        public string? NotarizationStatus { get; set; }
        public Guid? AttachmentId { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? RequestId { get; set; }
        //Relationship
        public virtual Order? Order { get; set; }
        public virtual Request? Request { get; set; }
        public virtual Notarization? Notarization { get; set; }
        public virtual DocumentType? DocumentType { get; set; }
        public virtual ICollection<AssignmentTranslation>? AssignmentTranslations { get; set; }
        public virtual ICollection<NotarizationDetail>? NotarizationDetails { get; set; }
    }
}

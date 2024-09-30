namespace Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public Guid? FirstLanguageId { get; set; }
        public Language? FirstLanguage { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Language? SecondLanguage { get; set; }
        public Guid? RequestId { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public string? FileType { get; set; }
        public int PageNumber { get; set; } = 0;
        public int NumberOfCopies { get; set; } = 0;
        public bool NotarizationRequest { get; set; } = false;
        public int NumberOfNotarizatedCopies { get; set; } = 0;
        public string? DocumentPath { get; set; }
        public decimal? Price { get; set; }
        //Relationship
        public virtual Notarization? Notarization { get; set; }
        public virtual Request? Request { get; set; }
        public virtual DocumentType? DocumentType { get; set; }
        public virtual Document? Document { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
    }
}

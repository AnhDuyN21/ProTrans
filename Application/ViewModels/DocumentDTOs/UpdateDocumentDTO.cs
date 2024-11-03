namespace Application.ViewModels.DocumentDTOs
{
    public class UpdateDocumentDTO
    {
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public string? UrlPath { get; set; }
        public string? FileType { get; set; }
        public int? PageNumber { get; set; }
        public int? NumberOfCopies { get; set; }
        public bool? NotarizationRequest { get; set; }
        public int? NumberOfNotarizatedCopies { get; set; }
        public string? TranslationStatus { get; set; }
        public string? NotarizationStatus { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
    }
}

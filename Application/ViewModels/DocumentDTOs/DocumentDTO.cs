namespace Application.ViewModels.DocumentDTOs
{
	public class DocumentDTO
	{
		public Guid Id { get; set; }
		public Guid? FirstLanguageId { get; set; }
		public Guid? SecondLanguageId { get; set; }
		public string? Code { get; set; }
		public string? UrlPath { get; set; }
		public string? FileType { get; set; }
		public int PageNumber { get; set; } = 0;
		public int NumberOfCopies { get; set; } = 0;
		public bool NotarizationRequest { get; set; } = false;
		public int NumberOfNotarizedCopies { get; set; } = 0;
		public string? TranslationStatus { get; set; }
		public string? NotarizationStatus { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? NotarizationId { get; set; }
		public Guid? DocumentTypeId { get; set; }
		public Guid? OrderId { get; set; }
		public Guid? RequestId { get; set; }
		public DocumentPriceDTO? DocumentPrice { get; set; }
		public List<DocumentHistoryDTO>? DocumentHistory { get; set; }
		public ChangedDocumentDTO? ChangedDocument { get; set; }
		public List<DocumentStatusDTO>? DocumentStatus { get; set; }
	}
}

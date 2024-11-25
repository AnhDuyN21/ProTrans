using System.Text.Json.Serialization;

namespace Application.ViewModels.DocumentDTOs
{
	public class CreateDocumentDTO
	{
		public Guid FirstLanguageId { get; set; }
		public Guid SecondLanguageId { get; set; }
		[JsonIgnore]
		public string? Code { get; set; }
		public string? UrlPath { get; set; }
		public string? FileType { get; set; }
		public int PageNumber { get; set; } = 0;
		public int NumberOfCopies { get; set; } = 0;
		public bool NotarizationRequest { get; set; } = false;
		public int NumberOfNotarizedCopies { get; set; } = 0;
		[JsonIgnore]
		public string? TranslationStatus { get; set; }
		[JsonIgnore]
		public string? NotarizationStatus { get; set; }
		public Guid NotarizationId { get; set; }
		public Guid DocumentTypeId { get; set; }
		[JsonIgnore]
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(7);
	}
}
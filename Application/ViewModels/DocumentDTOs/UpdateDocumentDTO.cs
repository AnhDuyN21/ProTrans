using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
	public class UpdateDocumentDTO
	{
		public Guid? FirstLanguageId { get; set; }
		public Guid? SecondLanguageId { get; set; }
		public string? UrlPath { get; set; }
		public string? FileType { get; set; }
		public int? PageNumber { get; set; } = 0;
		public int? NumberOfCopies { get; set; } = 0;
		public bool? NotarizationRequest { get; set; } = false;
		public int? NumberOfNotarizatedCopies { get; set; } = 0;
		public string? TranslationStatus { get; set; }
		public string? NotarizationStatus { get; set; }
		public Guid? AttachmentId { get; set; }
		public Guid? NotarizationId { get; set; }
		public Guid? DocumentTypeId { get; set; }
	}
}

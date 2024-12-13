using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
	public class ChangedDocumentDTO
	{
		public Guid DocumentId { get; set; }
		public string? OldPageNumber { get; set; }
		public string? OldDocumentTypeId { get; set; }
		public string? OldNotarizationId { get; set; }
	}
}

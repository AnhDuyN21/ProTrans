using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.DocumentDTOs
{
	public class DocumentStatusDTO
	{
		public Guid Id { get; set; }
		public string? Type { get; set; }
		public string? Status { get; set; }
		public DateTime? Time { get; set; }
		public Guid? DocumentId { get; set; }
		public virtual Document? Document { get; set; }
	}
}

using Application.ViewModels.DocumentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RequestDTOs
{
	public class RequestToQuoteDTO
	{
		public Guid Id { get; set; }
		public DateTime? Deadline { get; set; }
		public decimal? EstimatedPrice { get; set; }
		public string? Status { get; set; }
		public bool? IsConfirmed { get; set; }
		public bool? PickUpRequest { get; set; }
		public bool ShipRequest { get; set; }
		public bool IsDeleted { get; set; }
		public Guid? CustomerId { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public List<DocumentDTO>? Documents { get; set; }
	}
}

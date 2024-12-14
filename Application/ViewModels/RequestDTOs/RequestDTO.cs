using Application.ViewModels.DocumentDTOs;

namespace Application.ViewModels.RequestDTOs
{
	public class RequestDTO
	{
		public Guid Id { get; set; }
		public Guid? CustomerId { get; set; }
		public DateTime? Deadline { get; set; }
		public decimal? EstimatedPrice { get; set; }
		public string? Status { get; set; }
		public bool? IsConfirmed { get; set; }
		public bool? PickUpRequest { get; set; }
		public bool ShipRequest { get; set; }
		public bool IsDeleted { get; set; }
		public List<DocumentDTO>? Documents { get; set; }
		public DateTime CreateDate { get; set; }
	}
}

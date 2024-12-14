using Application.ViewModels.DocumentDTOs;

namespace Application.ViewModels.OrderDTOs
{
	public class OrderDTO
	{
		public Guid Id { get; set; }
		public string? OrderCode { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public bool ShipRequest { get; set; }
		public bool? PickUpRequest { get; set; }
		public DateTime? Deadline { get; set; }
		public decimal TotalPrice { get; set; }
		public string? Status { get; set; }
		public string? Reason { get; set; }
		public DateTime? CreatedDate { get; set; }
		public Guid? AgencyId { get; set; }
		public Guid? RequestId { get; set; }
		public List<DocumentDTO>? Documents { get; set; }
		public Guid? CustomerId { get; set; }
	}
}

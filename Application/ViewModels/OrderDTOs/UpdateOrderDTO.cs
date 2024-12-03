namespace Application.ViewModels.OrderDTOs
{
	public class UpdateOrderDTO
	{
		public Guid? RequestId { get; set; }
		public DateTime? Deadline { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Reason { get; set; }
		public string? Status { get; set; }
	}
}

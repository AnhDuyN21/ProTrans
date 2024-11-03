namespace Application.ViewModels.AssignmentShippingDTOs
{
	public class UpdateAssignmentShippingDTO
	{
		public Guid? ShipperId { get; set; }
		public Guid? OrderId { get; set; }
		public DateTime? Deadline { get; set; }
		public string? Type { get; set; }
		public string? Status { get; set; }
	}
}
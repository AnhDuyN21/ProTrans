namespace Application.ViewModels.AssignmentShippingDTOs
{
	public class CreateAssignmentShippingDTO
	{
		public Guid ShipperId { get; set; }
		public Guid OrderId { get; set; }
		public DateTime Deadline { get; set; }
		public string Type { get; set; }
	}
}

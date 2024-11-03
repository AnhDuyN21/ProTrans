namespace Application.ViewModels.AssignmentShippingDTOs
{
	public class CreateAssignmentShippingDTO
	{
		public Guid ShipperId { get; set; }
		public Guid OrderId { get; set; }
		public string? ImageUrl { get; set; }
	}
}

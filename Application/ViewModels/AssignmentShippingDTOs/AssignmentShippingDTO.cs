namespace Application.ViewModels.AssignmentShippingDTOs
{
	public class AssignmentShippingDTO
	{
		public Guid Id { get; set; }
		public Guid ShipperId { get; set; }
		public Guid OrderId { get; set; }
		public string ImageUrl { get; set; }
		public string Status { get; set; }
	}
}
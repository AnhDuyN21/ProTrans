namespace Application.ViewModels.FeedbackDTOs
{
	public class CUFeedbackDTO
	{
		public string? Message { get; set; }
		public Guid AccountId { get; set; }
		public Guid OrderId { get; set; }
	}
}

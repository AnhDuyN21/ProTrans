namespace Application.ViewModels.FeedbackDTOs
{
	public class FeedbackDTO
	{
		public Guid? Id { get; set; }
		public string? Message { get; set; }
		public Guid? AccountId { get; set; }
		public Guid? OrderId { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
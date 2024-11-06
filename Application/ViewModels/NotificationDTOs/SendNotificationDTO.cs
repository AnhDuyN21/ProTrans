namespace Application.ViewModels.NotificationDTOs
{
    public class SendNotificationDTO
    {
        public required Guid SpecId { get; set; }
        public required string Title { get; set; }
        public required string Message { get; set; }
        public required string Author { get; set; }
    }
}

namespace Application.ViewModels.NotificationDTOs
{
    public class SendLocationDTO
    {
        public required Guid SpecId { get; set; }
        public required Guid OrderId { get; set; }
        public required string Latitude { get; set; }
        public required string Longitude { get; set; }
    }
}

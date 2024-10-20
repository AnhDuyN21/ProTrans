namespace Application.ViewModels.ShippingDTOs
{
    public class CreateShippingDTO
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
        public string? ImageUrl { get; set; }
    }
}

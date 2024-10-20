namespace Application.ViewModels.RequestDTOs
{
    public class UpdateRequestDTO
    {
        public DateTime? Deadline { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public string? Status { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? PickUpRequest { get; set; }
        public bool ShipRequest { get; set; }
    }
}

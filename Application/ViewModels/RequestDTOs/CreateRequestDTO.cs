namespace Application.ViewModels.RequestDTOs
{
    public class CreateRequestDTO
    {
        public bool? PickUpRequest { get; set; }
        public bool ShipRequest { get; set; }
        public DateTime? Deadline { get; set; }
    }
}

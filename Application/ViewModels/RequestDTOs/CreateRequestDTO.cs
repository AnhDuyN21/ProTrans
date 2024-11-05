using Application.ViewModels.DocumentDTOs;

namespace Application.ViewModels.RequestDTOs
{
    public class CreateRequestDTO
    {
        public DateTime? Deadline { get; set; }
        public bool? PickUpRequest { get; set; }
        public bool ShipRequest { get; set; }
        public List<CreateDocumentDTO>? Documents { get; set; }
    }
}

using Application.ViewModels.DocumentDTOs;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.RequestDTOs
{
    public class UpdateRequestDTO
    {
        public DateTime? Deadline { get; set; }
        public decimal? EstimatedPrice { get; set; }
        [EnumDataType(typeof(RequestStatus),ErrorMessage = "Trạng thái của request phải là Quoted hoặc Cancel")]
        public string? Status { get; set; }
        public bool? PickUpRequest { get; set; }
        public bool ShipRequest { get; set; }
        public List<CreateDocumentDTO>? Documents { get; set; }
    }
}

using Application.ViewModels.DocumentDTOs;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.ViewModels.RequestDTOs
{
    public class UpdateRequestDTO
    {
        public DateTime? Deadline { get; set; }
        [JsonIgnore]
        public decimal? EstimatedPrice { get; set; }
        [EnumDataType(typeof(RequestStatus),ErrorMessage = "Trạng thái cập nhật của request phải là Quoted hoặc Cancel")]
        public string? Status { get; set; }
        [JsonIgnore]
        public bool? PickUpRequest { get; set; }
        [JsonIgnore]
        public bool ShipRequest { get; set; }
        public List<UpdateDocumentFromRequestDTO>? Documents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

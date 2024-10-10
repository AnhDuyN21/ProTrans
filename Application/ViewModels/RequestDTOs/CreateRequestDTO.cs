using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RequestDTOs
{
    public class CreateRequestDTO
    {
        public bool? PickUpRequest { get; set; }
        public bool ShipRequest { get; set; }
    }
}

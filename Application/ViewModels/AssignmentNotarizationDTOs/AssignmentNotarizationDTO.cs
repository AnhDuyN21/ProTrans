using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class AssignmentNotarizationDTO
    {
        public string ShipperName { get; set; }
        public string CustomerName { get; set; }
        public string? DocumentCode { get; set; }   
        public int NumberOfNotarization { get; set; }
        public string Status { get; set; }
        public DateOnly? Deadline { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class CUAssignmentNotarizationDTO
    {
        public Guid ShipperId { get; set; }
        public Guid DocumentId { get; set; }
        public int NumberOfNotarization { get; set; }
        public string Status { get; set; }
    }
}

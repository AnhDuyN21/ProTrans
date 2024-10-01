using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssignmentTranslationDTOs
{
    public class CUAssignmentTranslationDTO
    {
        public Guid? TranslatorId { get; set; }
        public string? Status { get; set; }
        public Guid? DocumentId { get; set; }
        public DateTime Deadline { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssignmentTranslationDTOs
{
    public class AssignmentTranslationDTO
    {
        public required string TranslatorName { get; set; }
        public required Guid? DocumentId { get; set; }
        public required string Status { get; set; }
        public DateTime Deadline { get; set; }
    }
}

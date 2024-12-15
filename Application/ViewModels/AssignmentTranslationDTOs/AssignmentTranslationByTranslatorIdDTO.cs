using Application.ViewModels.DocumentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssignmentTranslationDTOs
{
    public class AssignmentTranslationByTranslatorIdDTO
    {
        public Guid? Id { get; set; }
        public required Guid? TranslatorId { get; set; }
        public required Guid? DocumentId { get; set; }
        public required string Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DocumentDTO Document { get; set; }
    }
}

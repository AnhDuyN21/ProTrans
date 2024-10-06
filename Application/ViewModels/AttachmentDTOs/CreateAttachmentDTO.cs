using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AttachmentDTOs
{
    public class CreateAttachmentDTO
    {
        public List<IFormFile> AttachmentImages { get; set; } = new List<IFormFile>();
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public string FileType { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfCopies { get; set; }
        public bool NotarizationRequest { get; set; }
        public int NumberOfNotarizatedCopies { get; set; }
    }
}

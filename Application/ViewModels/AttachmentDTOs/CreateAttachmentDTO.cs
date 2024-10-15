using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.AttachmentDTOs
{
    public class CreateAttachmentDTO
    {
        public List<string> AttachmentImages { get; set; } = new List<string>();
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

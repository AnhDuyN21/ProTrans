namespace Application.ViewModels.AttachmentDTOs
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public Guid? FirstLanguageId { get; set; }
        public Guid? SecondLanguageId { get; set; }
        public Guid? RequestId { get; set; }
        public Guid? NotarizationId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public string? FileType { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfCopies { get; set; }
        public bool NotarizationRequest { get; set; }
        public int NumberOfNotarizatedCopies { get; set; }
        public string? DocumentPath { get; set; }
        public decimal? Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}

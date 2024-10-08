namespace Application.ViewModels.AssignmentTranslationDTOs
{
    public class AssignmentTranslationDTO
    {
        public required Guid? TranslatorId { get; set; }
        public required Guid? DocumentId { get; set; }
        public required string Status { get; set; }
        public DateTime Deadline { get; set; }
    }
}

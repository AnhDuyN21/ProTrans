namespace Application.ViewModels.AssignmentTranslationDTOs
{
    public class CUAssignmentTranslationDTO
    {
        public Guid? TranslatorId { get; set; }
        public Guid? DocumentId { get; set; }
        public DateTime Deadline { get; set; }
    }
}

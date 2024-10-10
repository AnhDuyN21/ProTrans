namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class AssignmentNotarizationDTO
    {
        public Guid ShipperId { get; set; }
        public Guid DocumentId { get; set; }
        public string CustomerName { get; set; }

        public int NumberOfNotarization { get; set; }
        public string Status { get; set; }
        public DateOnly? Deadline { get; set; }
    }
}

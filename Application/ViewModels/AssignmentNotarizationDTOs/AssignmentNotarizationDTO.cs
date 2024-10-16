using Domain.Entities;

namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class AssignmentNotarizationDTO
    {
        public Guid? Id { get; set; }
        public Guid ShipperId { get; set; }
        public Guid? OrderId { get; set; }
        public string? Code { get; set; }
        public int NumberOfNotarization { get; set; }
        public string Status { get; set; }
        public DateTime? Deadline { get; set; }
    }
}

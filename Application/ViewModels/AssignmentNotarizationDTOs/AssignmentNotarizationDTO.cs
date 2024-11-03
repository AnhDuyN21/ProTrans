using Domain.Entities;

namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class AssignmentNotarizationDTO
    {
        public Guid? Id { get; set; }
        public Guid ShipperId { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }
}

﻿namespace Application.ViewModels.AssignmentNotarizationDTOs
{
    public class CUAssignmentNotarizationDTO
    {
        public Guid ShipperId { get; set; }
        public DateTime Deadline { get; set; }
        public Guid[] DocumentId { get; set; }
    }
}

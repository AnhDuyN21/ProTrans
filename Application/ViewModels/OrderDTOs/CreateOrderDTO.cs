﻿using Application.ViewModels.DocumentDTOs;

namespace Application.ViewModels.OrderDTOs
{
    public class CreateOrderDTO
    {
        public Guid? PaymentId { get; set; }
        public Guid? RequestId { get; set; }
        public DateTime? Deadline { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<CreateDocumentDTO>? Documents { get; set; }
    }
}

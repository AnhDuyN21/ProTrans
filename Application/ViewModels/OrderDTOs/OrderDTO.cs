﻿namespace Application.ViewModels.OrderDTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid? PaymentId { get; set; }
        public Guid? AgencyId { get; set; }
        public Guid? RequestId { get; set; }
        public DateTime? Deadline { get; set; }
        public decimal TotalPrice { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
    }
}

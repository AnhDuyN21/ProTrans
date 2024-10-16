﻿namespace Domain.Entities
{
    public class Order : BaseEntity
    {
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
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual Agency? Agency { get; set; }
        public virtual Request? Request { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }
        public virtual Transaction? Transaction { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
    }
}

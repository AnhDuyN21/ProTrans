﻿namespace Domain.Entities
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

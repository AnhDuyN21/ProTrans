﻿namespace Application.ViewModels.ShippingDTOs
{
    public class CUShippingDTO
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsShipped { get; set; } = false;
    }
}
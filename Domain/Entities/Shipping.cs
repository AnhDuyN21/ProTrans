namespace Domain.Entities
{
    public class Shipping : BaseEntity
    {
        public Guid ShipperId { get; set; }
        public Guid OrderId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsShipped { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Order? Order { get; set; }
    }
}

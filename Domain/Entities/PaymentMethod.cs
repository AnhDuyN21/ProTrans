namespace Domain.Entities
{
    public class PaymentMethod
    {
        //Field
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Relationship
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

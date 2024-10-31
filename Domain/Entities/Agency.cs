namespace Domain.Entities
{
    public class Agency : BaseEntity
    {
        //Field
        public string Name { get; set; }
        public string Address { get; set; }
        //Relationship
        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}

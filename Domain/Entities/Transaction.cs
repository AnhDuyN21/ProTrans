namespace Domain.Entities
{
    public class Transaction
    {
        //Field
        public Guid Id { get; set; }
        //Foreignkey
        public Guid AccountId { get; set; }
        public Guid OrderId { get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
        public virtual Order? Order { get; set; }
    }
}
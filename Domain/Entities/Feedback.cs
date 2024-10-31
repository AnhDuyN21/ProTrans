namespace Domain.Entities
{
    public class Feedback : BaseEntity
    {
        //Foreignkey
        public Guid AccountId { get; set; }
        public Guid OrderId { get; set; }
        //Field
        public string? Message { get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
        public virtual Order? Order { get; set; }
    }
}
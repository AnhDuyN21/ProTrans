namespace Domain.Entities
{
    public class Role
    {
        //Field
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Relationship
        public virtual ICollection<Account>? Accounts { get; set; }
    }
}

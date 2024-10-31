namespace Domain.Entities
{
    public class AssignmentNotarization : BaseEntity
    {
        //Field
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        //Foreignkey
        public Guid ShipperId { get; set; }
        //Relationship
        public virtual Account? Shipper { get; set; }
        public virtual ICollection<NotarizationDetail>? NotarizationDetails { get; set; }
    }
}

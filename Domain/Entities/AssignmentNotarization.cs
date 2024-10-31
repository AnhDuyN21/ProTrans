namespace Domain.Entities
{
    public class AssignmentNotarization : BaseEntity
    {
        public Guid ShipperId { get; set; }
        public Guid DocumentId { get; set; }
        public int NumberOfNotarization { get; set; }
        public string Status { get; set; }
        //Relationship
        public virtual Account? Shipper { get; set; }
        public virtual Document? Document { get; set; }
        public virtual ICollection<NotarizationDetail>? NotarizationDetails { get; set; }
    }
}

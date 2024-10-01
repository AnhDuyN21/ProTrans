namespace Domain.Entities
{
    public class Notarization : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}

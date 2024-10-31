namespace Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; }
        public decimal PriceFactor { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }
    }
}

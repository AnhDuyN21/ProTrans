namespace Domain.Entities
{
    public class DocumentType : BaseEntity
    {
        //Field
        public string Name { get; set; }
        public decimal PriceFactor { get; set; }
        //Relationship
        public virtual ICollection<Document>? Documents { get; set; }
    }
}

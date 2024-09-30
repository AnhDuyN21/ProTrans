namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public Guid? AttachmentId { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Attachment? Attachment { get; set; }
    }
}

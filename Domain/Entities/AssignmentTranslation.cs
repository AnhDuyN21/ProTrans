namespace Domain.Entities
{
    public class AssignmentTranslation : BaseEntity
    {
        //Field
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string? UrlPath { get; set; }
        //Foreignkey
        public Guid? TranslatorId { get; set; }
        public Guid? DocumentId { get; set; }
        //Relationship
        public virtual Account? Translator { get; set; }
        public virtual Document? Document { get; set; }
    }
}

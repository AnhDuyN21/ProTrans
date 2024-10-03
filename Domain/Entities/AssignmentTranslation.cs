namespace Domain.Entities
{
    public class AssignmentTranslation : BaseEntity
    {
        public Guid? TranslatorId { get; set; }
        public Guid? DocumentId { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public virtual Account? Translator { get; set; }
        public virtual Document? Document { get; set; }
    }
}

namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        //Field
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        //Foreignkey
        public Guid? RoleId { get; set; }
        public Guid? AgencyId { get; set; }
        //Relationship
        public virtual Agency? Agency { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Request>? CustomerRequests { get; set; }
        public virtual ICollection<Request>? ShipperRequests { get; set; }
        public virtual ICollection<TranslationSkill>? TranslationSkills { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }
        public virtual ICollection<AssignmentShipping>? AssignmentShippings { get; set; }
        public virtual ICollection<AssignmentTranslation>? AssignmentTranslations { get; set; }
        public virtual ICollection<AssignmentNotarization>? AssignmentNotarizations { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }

    }
}

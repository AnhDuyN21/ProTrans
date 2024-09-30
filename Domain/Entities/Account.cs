namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? AgencyId { get; set; }
        //Entity Relationship
        public virtual Agency? Agency { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Request>? CustomerRequests { get; set; }
        public virtual ICollection<Request>? ShipperRequests { get; set; }
        public virtual ICollection<TranslatorSkill>? TranslatorSkills { get; set; }
        public virtual ICollection<FeedBack>? FeedBacks { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
        public virtual ICollection<AssignmentTranslation>? AssignmentTranslations { get; set; }
        public virtual ICollection<AssignmentNotarization>? AssignmentNotarizations { get; set; }
        public virtual ICollection<TransactionsHistory>? TransactionsHistory { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }

    }
}

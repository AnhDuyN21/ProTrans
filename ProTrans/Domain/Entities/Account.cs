using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? Dob { get; set; }
        public string? Gender { get; set; }
        public string? RoleId { get; set; }
        public string? AgencyId { get; set; }

        public virtual Agency? Agency { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Request>? Requests { get; set; }
        public virtual ICollection<TranslatorSkill>? TranslatorSkills { get; set; }
        public virtual ICollection<FeedBack>? FeedBacks { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
        public virtual ICollection<Shipping>? Shippings { get; set; }
        public virtual ICollection<AssignmentTranslation>? AssignmentTranslations { get; set; }
        public virtual ICollection<AssignmentNotarization>? AssignmentNotarizations { get; set; }
        public virtual ICollection<TransactionsHistory>? TransactionsHistory { get; set; }
    }
}

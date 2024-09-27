using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TranslatorSkill : BaseEntity
    {
        public Guid TranslatorId { get; set; }
        public Guid LanguageId { get; set; }
        public string? CertificateUrl {  get; set; }
        public virtual Account? Account { get; set; }
        public virtual Language? Language { get; set; }
    }
}

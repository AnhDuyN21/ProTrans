using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TranslationSkill : BaseEntity
    {
        //Field
        public string? CertificateUrl { get; set; }
        //Foreignkey
        public Guid TranslatorId { get; set; }
        public Guid LanguageId { get; set; }
        //Relationship
        public virtual Account? Translator { get; set; }
        public virtual Language? Language { get; set; }
    }
}

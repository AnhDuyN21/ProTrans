using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TranslatorSkill
    {
        public Guid Id { get; set; }
        public Guid TranslatorId { get; set; }
        public Guid LanguageId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Language? Language { get; set; }
    }
}

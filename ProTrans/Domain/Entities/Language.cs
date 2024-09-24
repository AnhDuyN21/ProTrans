using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Name {  get; set; }
        public ICollection<QuotePrice>? FirstLanguage_QuotePrice { get; set; }
        public ICollection<QuotePrice>? SecondLanguage_QuotePrice { get; set; }
        public virtual ICollection<TranslatorSkill>? TranslatorSkills { get; set; }
        //public virtual ICollection<QuotePrice>? QuotePrices { get; set; }
        public virtual ICollection<Document>? FirstLanguage_Document { get; set; }
        public virtual ICollection<Document>? SecondLanguage_Document { get; set; }
    }
}

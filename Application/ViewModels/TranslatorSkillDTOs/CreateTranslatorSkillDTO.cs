using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TranslatorSkillDTOs
{
    public class CreateTranslatorSkillDTO
    {
        public Guid LanguageId { get; set; }
        public string? CertificateUrl { get; set; }
    }
}

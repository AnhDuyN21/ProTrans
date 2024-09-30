namespace Application.ViewModels.TranslatorSkillDTOs
{
    public class CUTranslatorSkillDTO
    {
        public Guid TranslatorId { get; set; }
        public Guid LanguageId { get; set; }
        public string? CertificateUrl { get; set; }
    }
}

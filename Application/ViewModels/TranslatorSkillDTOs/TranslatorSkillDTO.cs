namespace Application.ViewModels.TranslatorSkillDTOs
{
    public class TranslatorSkillDTO
    {
        public Guid Id { get; set; }
        public string TranslatorName { get; set; }
        public string LanguageName { get; set; }
        public string? CertificateUrl { get; set; }
    }
}

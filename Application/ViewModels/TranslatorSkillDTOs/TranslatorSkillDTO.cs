namespace Application.ViewModels.TranslatorSkillDTOs
{
    public class TranslatorSkillDTO
    {
        public Guid Id { get; set; }
        public Guid TranslatorId { get; set; }
        public Guid LanguageId { get; set; }
        public string? CertificateUrl { get; set; }
    }
}

using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.Image;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Application.Interfaces.InterfaceRepositories.Attachment;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public INotarizationRepository NotarizationRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IQuotePriceRepository QuotePriceRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public ITranslatorSkillRepository TranslatorSkillRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public IAssignmentTranslationRepository AssignmentTranslationRepository { get; }
        public IAssignmentNotarizationRepository AssignmentNotarizationRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }
        public Task<int> SaveChangeAsync();

    }
}

using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Application.Interfaces.InterfaceRepositories.Image;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;
using Application.Interfaces.InterfaceRepositories.Documents;
using Application.Interfaces.InterfaceRepositories.Orders;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Application.Interfaces.InterfaceRepositories.Attachment;
using Application.Interfaces.InterfaceRepositories.Shippings;
using Application.Interfaces.InterfaceRepositories.PaymentMethods;
using Application.Interfaces.InterfaceRepositories.DocumentType;
using Application.Interfaces.InterfaceRepositories.Transactions;

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
        public IFeedbackRepository FeedbackRepository { get; }
        public IAssignmentTranslationRepository AssignmentTranslationRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public IDocumentTypeRepository DocumentTypeRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IAssignmentNotarizationRepository AssignmentNotarizationRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }
        public IShippingRepository ShippingRepository { get; }
        public IPaymentMethodRepository PaymenMethodRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public Task<int> SaveChangeAsync();

    }
}

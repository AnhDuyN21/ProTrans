using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.Agency;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.DocumentHistory;
using Application.Interfaces.InterfaceRepositories.DocumentPrice;
using Application.Interfaces.InterfaceRepositories.Documents;
using Application.Interfaces.InterfaceRepositories.DocumentType;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.NotarizationDetail;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.Orders;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Request;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.IAssignmentShippings;
using Application.Interfaces.InterfaceRepositories.Transactions;
using Application.Interfaces.InterfaceRepositories.TranslationSkill;
using Application.Interfaces.InterfaceRepositories;
using Application.Interfaces.InterfaceRepositories.ImageShippings;
using Application.Interfaces.InterfaceRepositories.Distance;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IAgencyRepository AgencyRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public INotarizationRepository NotarizationRepository { get; }
        public IQuotePriceRepository QuotePriceRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public ITranslatorSkillRepository TranslatorSkillRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IAssignmentTranslationRepository AssignmentTranslationRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public IDocumentTypeRepository DocumentTypeRepository { get; }
        public IDocumentHistoryRepository DocumentHistoryRepository { get; }
        public IDocumentPriceRepository DocumentPriceRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IAssignmentNotarizationRepository AssignmentNotarizationRepository { get; }
        public IAssignmentShippingRepository AssignmentShippingRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IRequestRepository RequestRepository { get; }
        public INotarizationDetailRepository NotarizationDetailRepository { get; }
        public ISendMailRepository SendMailRepository { get; }
        public IImageShippingRepository ImageShippingRepository { get; }
        public IDistanceRepository DistanceRepository { get; }
        public Task<int> SaveChangeAsync();

    }
}

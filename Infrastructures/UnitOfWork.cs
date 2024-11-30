using Application.Interfaces;
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
using Application.Interfaces.InterfaceRepositories.DocumentStatus;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly INotarizationRepository _notarizationRepository;
        private readonly IQuotePriceRepository _quotePriceRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ITranslatorSkillRepository _translatorSkillRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAssignmentTranslationRepository _assignmentTranslationRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAssignmentNotarizationRepository _assignmentNotarizationRepository;
        private readonly IAssignmentShippingRepository _shippingRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IDocumentHistoryRepository _documentHistoryRepository;
        private readonly IDocumentPriceRepository _documentPriceRepository;
        private readonly INotarizationDetailRepository _notarizationDetailRepository;
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IImageShippingRepository _imageShippingRepository;
        private readonly IDistanceRepository _distanceRepository;
        private readonly IDocumentStatusRepository _documentStatusRepository;
        
        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IRoleRepository roleRepository
            , INotarizationRepository notarizationRepository, IQuotePriceRepository quotePriceRepository, ILanguageRepository languageRepository,
            ITranslatorSkillRepository translatorSkillRepository, INotificationRepository notificationRepository,
            IAssignmentTranslationRepository assignmentTranslationRepository,
            IDocumentRepository documentReository, IOrderRepository orderRepository,
            IAssignmentNotarizationRepository assignmentNotarizationRepository,
            IAssignmentShippingRepository shippingRepository, IFeedbackRepository feedbackRepository,
            IDocumentTypeRepository documentTypeRepository,
            ITransactionRepository transactionRepository, IRequestRepository requestRepository, IAgencyRepository agencyRepository,
            IDocumentHistoryRepository documentHistoryRepository,
            IDocumentPriceRepository documentPriceRepository, INotarizationDetailRepository notarizationDetailRepository,
            ISendMailRepository sendMailRepository, IImageShippingRepository imageShippingRepository,
            IDistanceRepository distanceRepository, IDocumentStatusRepository documentStatusRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _notarizationRepository = notarizationRepository;
            _quotePriceRepository = quotePriceRepository;
            _translatorSkillRepository = translatorSkillRepository;
            _notificationRepository = notificationRepository;
            _languageRepository = languageRepository;
            _assignmentTranslationRepository = assignmentTranslationRepository;
            _feedbackRepository = feedbackRepository;
            _documentRepository = documentReository;
            _documentTypeRepository = documentTypeRepository;
            _orderRepository = orderRepository;
            _assignmentNotarizationRepository = assignmentNotarizationRepository;
            _shippingRepository = shippingRepository;
            _transactionRepository = transactionRepository;
            _requestRepository = requestRepository;
            _agencyRepository = agencyRepository;
            _documentHistoryRepository = documentHistoryRepository;
            _documentPriceRepository = documentPriceRepository;
            _notarizationDetailRepository = notarizationDetailRepository;
            _sendMailRepository = sendMailRepository;
            _imageShippingRepository = imageShippingRepository;
            _distanceRepository = distanceRepository;
            _documentStatusRepository = documentStatusRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IAgencyRepository AgencyRepository => _agencyRepository;
        public IRoleRepository RoleRepository => _roleRepository;
        public INotarizationRepository NotarizationRepository => _notarizationRepository;
        public IQuotePriceRepository QuotePriceRepository => _quotePriceRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public ITranslatorSkillRepository TranslatorSkillRepository => _translatorSkillRepository;
        public INotificationRepository NotificationRepository => _notificationRepository;
        public IFeedbackRepository FeedbackRepository => _feedbackRepository;
        public IAssignmentTranslationRepository AssignmentTranslationRepository => _assignmentTranslationRepository;
        public IDocumentRepository DocumentRepository => _documentRepository;
        public IDocumentTypeRepository DocumentTypeRepository => _documentTypeRepository;
        public IDocumentHistoryRepository DocumentHistoryRepository => _documentHistoryRepository;
        public IDocumentPriceRepository DocumentPriceRepository => _documentPriceRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IAssignmentNotarizationRepository AssignmentNotarizationRepository => _assignmentNotarizationRepository;
        public IAssignmentShippingRepository AssignmentShippingRepository => _shippingRepository;
        public ITransactionRepository TransactionRepository => _transactionRepository;
        public IRequestRepository RequestRepository => _requestRepository;
        public ISendMailRepository SendMailRepository => _sendMailRepository;
        public INotarizationDetailRepository NotarizationDetailRepository => _notarizationDetailRepository;
        public IImageShippingRepository ImageShippingRepository => _imageShippingRepository;
        public IDistanceRepository DistanceRepository => _distanceRepository;
        public IDocumentStatusRepository DocumentStatusRepository => _documentStatusRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
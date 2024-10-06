using Application.Interfaces;
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
using Application.Interfaces.InterfaceRepositories.Shippings;
using Application.Interfaces.InterfaceRepositories.PaymentMethods;

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
        private readonly IOrderRepository _orderRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IAssignmentNotarizationRepository _assignmentNotarizationRepository;
        private readonly IShippingRepository _shippingRepository;
        private readonly IPaymenMethodRepository _paymentMethodRepository;
        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IRoleRepository roleRepository
            , INotarizationRepository notarizationRepository, IQuotePriceRepository quotePriceRepository, ILanguageRepository languageRepository,
            ITranslatorSkillRepository translatorSkillRepository, INotificationRepository notificationRepository,
            IAssignmentTranslationRepository assignmentTranslationRepository,
            IDocumentRepository documentReository, IOrderRepository orderRepository,
            IImageRepository imageRepository, IAssignmentNotarizationRepository assignmentNotarizationRepository,
            IShippingRepository shippingRepository, IFeedbackRepository feedbackRepository,
            IPaymenMethodRepository paymenMethodRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _notarizationRepository = notarizationRepository;
            _imageRepository = imageRepository;
            _quotePriceRepository = quotePriceRepository;
            _translatorSkillRepository = translatorSkillRepository;
            _notificationRepository = notificationRepository;
            _languageRepository = languageRepository;
            _assignmentTranslationRepository = assignmentTranslationRepository;
            _feedbackRepository = feedbackRepository;
            _documentRepository = documentReository;
            _orderRepository = orderRepository;
            _assignmentNotarizationRepository = assignmentNotarizationRepository;
            _shippingRepository = shippingRepository;
            _paymentMethodRepository = paymenMethodRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IRoleRepository RoleRepository => _roleRepository;
        public INotarizationRepository NotarizationRepository => _notarizationRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IQuotePriceRepository QuotePriceRepository => _quotePriceRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public ITranslatorSkillRepository TranslatorSkillRepository => _translatorSkillRepository;
        public INotificationRepository NotificationRepository => _notificationRepository;
        public IFeedbackRepository FeedbackRepository => _feedbackRepository;
        public IAssignmentTranslationRepository AssignmentTranslationRepository => _assignmentTranslationRepository;
        public IDocumentRepository DocumentRepository => _documentRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IAssignmentNotarizationRepository AssignmentNotarizationRepository => _assignmentNotarizationRepository;
        public IShippingRepository ShippingRepository => _shippingRepository;
        public IPaymenMethodRepository PaymenMethodRepository => _paymentMethodRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}

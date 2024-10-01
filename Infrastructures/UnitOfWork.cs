using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.Image;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;

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
        private readonly IAssignmentTranslationRepository _assignmentTranslationRepository;
        private readonly IImageRepository _imageRepository;
        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, IRoleRepository roleRepository
            , INotarizationRepository notarizationRepository, IQuotePriceRepository quotePriceRepository, ILanguageRepository languageRepository,
            ITranslatorSkillRepository translatorSkillRepository, INotificationRepository notificationRepository,
            IAssignmentTranslationRepository assignmentTranslationRepository, IImageRepository imageRepository)
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
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IRoleRepository RoleRepository => _roleRepository;
        public INotarizationRepository NotarizationRepository => _notarizationRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IQuotePriceRepository QuotePriceRepository => _quotePriceRepository;
        public ILanguageRepository LanguageRepository => _languageRepository;
        public ITranslatorSkillRepository TranslatorSkillRepository => _translatorSkillRepository;
        public INotificationRepository NotificationRepository => _notificationRepository;
        public IAssignmentTranslationRepository AssignmentTranslationRepository => _assignmentTranslationRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}

using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.AssignmentNotarization;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.Attachment;
using Application.Interfaces.InterfaceRepositories.Documents;
using Application.Interfaces.InterfaceRepositories.DocumentType;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Application.Interfaces.InterfaceRepositories.Image;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.Orders;
using Application.Interfaces.InterfaceRepositories.PaymentMethods;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.Shippings;
using Application.Interfaces.InterfaceRepositories.Transactions;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;
using Application.Interfaces.InterfaceServices.Account;
using Application.Interfaces.InterfaceServices.AssignmentNotarization;
using Application.Interfaces.InterfaceServices.AssignmentTranslation;
using Application.Interfaces.InterfaceServices.Attachment;
using Application.Interfaces.InterfaceServices.Documents;
using Application.Interfaces.InterfaceServices.DocumentType;
using Application.Interfaces.InterfaceServices.Feedbacks;
using Application.Interfaces.InterfaceServices.Firebase;
using Application.Interfaces.InterfaceServices.Image;
using Application.Interfaces.InterfaceServices.Language;
using Application.Interfaces.InterfaceServices.Notarization;
using Application.Interfaces.InterfaceServices.Notification;
using Application.Interfaces.InterfaceServices.Orders;
using Application.Interfaces.InterfaceServices.PaymentMethods;
using Application.Interfaces.InterfaceServices.QuotePrice;
using Application.Interfaces.InterfaceServices.Shippings;
using Application.Interfaces.InterfaceServices.Transactions;
using Application.Interfaces.InterfaceServices.TranslatorSkill;
using Application.Services;
using Application.Services.Account;
using Application.Services.AssignmentNotarization;
using Application.Services.AssignmentTranslation;
using Application.Services.Attachment;
using Application.Services.Documents;
using Application.Services.DocumentType;
using Application.Services.Feedbacks;
using Application.Services.Firebase;
using Application.Services.Image;
using Application.Services.Language;
using Application.Services.Notarization;
using Application.Services.Notification;
using Application.Services.Orders;
using Application.Services.PaymentMethods;
using Application.Services.QuotePrice;
using Application.Services.Shippings;
using Application.Services.Transactions;
using Application.Services.TranslatorSkill;
using Google.Cloud.Storage.V1;
using Infrastructures.Mappers;
using Infrastructures.Repositories.Account;
using Infrastructures.Repositories.AssignmentNotarization;
using Infrastructures.Repositories.AssignmentTranslation;
using Infrastructures.Repositories.Attachment;
using Infrastructures.Repositories.Documents;
using Infrastructures.Repositories.DocumentType;
using Infrastructures.Repositories.Feedbacks;
using Infrastructures.Repositories.Image;
using Infrastructures.Repositories.Language;
using Infrastructures.Repositories.Notarization;
using Infrastructures.Repositories.Notification;
using Infrastructures.Repositories.Orders;
using Infrastructures.Repositories.PaymentMethods;
using Infrastructures.Repositories.Role;
using Infrastructures.Repositories.Shippings;
using Infrastructures.Repositories.Transactions;
using Infrastructures.Repositories.TranslatorSkill;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            //Attachments
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            //Accounts
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            //Roles
            services.AddScoped<IRoleRepository, RoleRepository>();

            //QuotePrices
            services.AddScoped<IQuotePriceRepository, QuotePriceRepository>();
            services.AddScoped<IQuotePriceService, QuotePriceService>();

            //Languages
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ILanguageService, LanguageService>();

            //TranslatorSkills
            services.AddScoped<ITranslatorSkillRepository, TranslatorSkillRepository>();
            services.AddScoped<ITranslatorSkillService, TranslatorSkillService>();

            //Notifications
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            //Notarizations
            services.AddScoped<INotarizationRepository, NotarizationRepository>();
            services.AddScoped<INotarizationService, NotarizationService>();

            //Feedbacks
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            //AssignmentTranslations
            services.AddScoped<IAssignmentTranslationRepository, AssignmentTranslationRepository>();
            services.AddScoped<IAssignmentTranslationService, AssignmentTranslationService>();

            //Documents
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();

            //DocumentType
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();

            //Orders
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            //AssignmentNotarizations
            services.AddScoped<IAssignmentNotarizationRepository, AssignmentNotarizationRepository>();
            services.AddScoped<IAssignmentNotarizationService, AssignmentNotarizationService>();

            //Firebases
            services.AddSingleton(opt => StorageClient.Create());
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();

            //Images
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();

            //Shippings
            services.AddScoped<IShippingRepository, ShippingResopitory>();
            services.AddScoped<IShippingService, ShippingService>();
            //PaymentMethods
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            //Transactions
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(databaseConnection);
            });
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}

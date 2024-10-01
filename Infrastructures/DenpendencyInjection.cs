using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Application.Interfaces.InterfaceRepositories.Language;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Application.Interfaces.InterfaceRepositories.Notification;
using Application.Interfaces.InterfaceRepositories.QuotePrice;
using Application.Interfaces.InterfaceRepositories.Role;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;
using Application.Interfaces.InterfaceServices.Account;
using Application.Interfaces.InterfaceServices.AssignmentTranslation;
using Application.Interfaces.InterfaceServices.Language;
using Application.Interfaces.InterfaceServices.Notarization;
using Application.Interfaces.InterfaceServices.Notification;
using Application.Interfaces.InterfaceServices.QuotePrice;
using Application.Interfaces.InterfaceServices.TranslatorSkill;
using Application.Services;
using Application.Services.Account;
using Application.Services.AssignmentTranslation;
using Application.Services.Language;
using Application.Services.Notarization;
using Application.Services.Notification;
using Application.Services.QuotePrice;
using Application.Services.TranslatorSkill;
using Infrastructures.Mappers;
using Infrastructures.Repositories.Account;
using Infrastructures.Repositories.AssignmentTranslation;
using Infrastructures.Repositories.Language;
using Infrastructures.Repositories.Notarization;
using Infrastructures.Repositories.Notification;
using Infrastructures.Repositories.Role;
using Infrastructures.Repositories.TranslatorSkill;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
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
            //AssignmentTranslations
            services.AddScoped<IAssignmentTranslationRepository, AssignmentTranslationRepository>();
            services.AddScoped<IAssignmentTranslationService, AssignmentTranslationService>();

            //Firebases
            services.AddSingleton(opt => StorageClient.Create());
            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();

            //Images
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();

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

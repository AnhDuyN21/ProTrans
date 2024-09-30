using Application.Interfaces.InterfaceRepositories.Account;
using Application.Interfaces;
using Infrastructures.Repositories.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.InterfaceServices.Account;
using Application.Services.Account;
using Application.Services;
using Infrastructures.Mappers;
using Application.Interfaces.InterfaceRepositories.Role;
using Infrastructures.Repositories.Role;
using Application.Interfaces.InterfaceRepositories.Notarization;
using Infrastructures.Repositories.Notarization;
using Application.Interfaces.InterfaceServices.Notarization;
using Application.Services.Notarization;
using Application.Interfaces.InterfaceServices.Firebase;
using Application.Services.Firebase;
using Google.Cloud.Storage.V1;
using Application.Interfaces.InterfaceRepositories.Image;
using Infrastructures.Repositories.Image;
using Application.Interfaces.InterfaceServices.Image;
using Application.Services.Image;

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

            //Notarizations
            services.AddScoped<INotarizationRepository, NotarizationRepository>();
            services.AddScoped<INotarizationService, NotarizationService>();

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

using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AssignmentShippingDTOs;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.OrderDTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Diagnostics;
using WebAPI.Services;
using WebAPI.Validations.AccountValidations;
using WebAPI.Validations.AssignmentShippingValidations;
using WebAPI.Validations.DocumentValidations;
using WebAPI.Validations.OrderValidations;

namespace WebAPI
{
	public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidation();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            //Fluent Validator

            //AccountDTOs
            services.AddTransient<IValidator<RegisterDTO>, RegisterDTOValidation>();

            //Documents
            services.AddTransient<IValidator<UpdateDocumentDTO>, DocumentValidation>();
            services.AddTransient<IValidator<CreateDocumentDTO>, CreateDocumentValidation>();

            //Orders
            services.AddTransient<IValidator<UpdateOrderDTO>, OrderValidation>();

            //Shippings
            services.AddTransient<IValidator<UpdateAssignmentShippingDTO>, AssignmentShippingValidation>();


            services.AddMemoryCache();
            return services;
        }
    }
}

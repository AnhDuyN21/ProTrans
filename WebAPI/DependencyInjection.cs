﻿using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.AttachmentDTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Diagnostics;
using WebAPI.Services;
using WebAPI.Validations.AccountValidations;
using WebAPI.Validations.AttachmentValidations;

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

            //AttachmentDTOs
            services.AddTransient<IValidator<CreateAttachmentDTO>, CreateAttachmentDTOValidation>();

            services.AddMemoryCache();
            return services;
        }
    }
}

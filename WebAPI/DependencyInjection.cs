﻿using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructures;
using System.Diagnostics;
using WebAPI.Services;
using WebAPI.Validations.AccountValidations;

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

            services.AddMemoryCache();
            return services;
        }
    }
}
﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Core.Application.Pipelines.Validation;
using System.Reflection;
using FluentValidation;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.Developers.Rules;
using Microsoft.AspNetCore.Http;
using Core.Application.Pipelines.Authorization;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<ProgrammingLanguageBusinessRules>();
        services.AddScoped<GithubProfileBusinessRules>();
        services.AddScoped<DeveloperBusinessRules>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        return services;
    }
}

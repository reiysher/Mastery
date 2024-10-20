﻿using System.Reflection;
using FluentValidation;
using Mastery.Common.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommonApplication(this IServiceCollection services, params Assembly[] moduleAssemblies)
    {

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(moduleAssemblies);

            options.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}

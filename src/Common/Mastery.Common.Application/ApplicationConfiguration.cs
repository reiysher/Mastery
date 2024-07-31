using System.Reflection;
using FluentValidation;
using Mastery.Common.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] moduleAssemblies)
    {

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(moduleAssemblies);

            options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}

using System.Reflection;
using FluentValidation;
using Mastery.Common.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[] moduleAssemblies)
    {

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(moduleAssemblies);
            
            options.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}

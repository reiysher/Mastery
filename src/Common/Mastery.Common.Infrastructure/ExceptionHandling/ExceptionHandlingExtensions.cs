using Mastery.Common.Infrastructure.ExceptionHandling.ExceptionDetails;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Mastery.Common.Infrastructure.ExceptionHandling;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection RegisterExceptionHandling(
        this IServiceCollection services,
        Action<ExceptionHandlingOptions>? options = null)
    {
        if (options != null)
        {
            services.Configure(options);
        }

        services.AddTransient<ExceptionDetailsProvider>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<ExceptionHandlingOptions>, ExceptionHandlingOptionsSetup>());

        return services;
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Mastery.Common.Infrastructure.ExceptionHandling;

internal sealed class ExceptionHandlingOptionsSetup : IConfigureOptions<ExceptionHandlingOptions>
{
    private const string DefaultExceptionDetailsPropertyName = "exceptionDetails";
    private const string DefaultTraceIdPropertyName = "traceId";

    public void Configure(ExceptionHandlingOptions options)
    {
        options.IncludeExceptionDetails ??= IncludeExceptionDetails;

        if (string.IsNullOrWhiteSpace(options.TraceIdPropertyName))
        {
            options.TraceIdPropertyName = DefaultTraceIdPropertyName;
        }

        if (string.IsNullOrWhiteSpace(options.ExceptionDetailsPropertyName))
        {
            options.ExceptionDetailsPropertyName = DefaultExceptionDetailsPropertyName;
        }
    }

    private static bool IncludeExceptionDetails(HttpContext context, Exception exception)
    {
        return context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment();
    }
}

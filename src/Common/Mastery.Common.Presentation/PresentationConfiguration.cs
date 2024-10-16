using Mastery.Common.Application.Security;
using Mastery.Common.Presentation.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Common.Presentation;

public static class PresentationConfiguration
{
    public static IServiceCollection AddCommonPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserContext, CurrentUserContext>();

        return services;
    }
}

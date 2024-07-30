using Mastery.Modules.Career.Application.Companies.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Career.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<CreateCompanyCommandHandler>();
        });

        return services;
    }
}

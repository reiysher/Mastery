using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Common.ApplicationBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationBus(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
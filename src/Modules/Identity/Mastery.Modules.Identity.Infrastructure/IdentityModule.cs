using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Application.Users;
using Mastery.Modules.Identity.Domain.Users;
using Mastery.Modules.Identity.Infrastructure.Authentication;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;
using Mastery.Modules.Identity.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Identity.Infrastructure;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
        services.AddInfrastructure(databaseConnectionString);
        
        return services;
    }

    private static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        string databaseConnectionString)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(databaseConnectionString, builder =>
            {
                builder.MigrationsHistoryTable("ef_migrations_history");
                builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityDbContext>());

        return services;
    }
}

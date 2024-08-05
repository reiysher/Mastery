using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Presentation;
using Mastery.Modules.Users.Application.Abstractions.Data;
using Mastery.Modules.Users.Application.Users;
using Mastery.Modules.Users.Domain.Users;
using Mastery.Modules.Users.Infrastructure.Authentication;
using Mastery.Modules.Users.Infrastructure.Persistence;
using Mastery.Modules.Users.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Users.Infrastructure;

public static class IdentityModule
{
    public static IServiceCollection AddUsersModule(
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

using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Application.Identity;
using Mastery.Modules.Identity.Domain.Permissions;
using Mastery.Modules.Identity.Domain.Roles;
using Mastery.Modules.Identity.Domain.Users;
using Mastery.Modules.Identity.Infrastructure.Authentication;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;
using Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;
using Mastery.Modules.Identity.Presentation;
using Microsoft.AspNetCore.Identity;
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
        services.AddOptions<JwtSettings>()
            .BindConfiguration(JwtSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddIdentity<User, Role>();
        services.AddTransient<IUserStore<User>, UserStore>();
        services.AddTransient<IRoleStore<Role>, RoleStore>();
        services.AddTransient<ITokenService, TokenService>();

        services.AddScoped<ISeeder, RolesSeeder>();
        services.AddScoped<ISeeder, UsersSeeder>();

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
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityDbContext>());

        return services;
    }
}

using Mastery.Common.Application;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Application.Users;
using Mastery.Modules.Identity.Domain.Users;
using Mastery.Modules.Identity.Infrastructure.Authentication;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Identity.Infrastructure;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IdentityModuleOptions options = configuration
            .GetSection(IdentityModuleOptions.SectionName)
            .Get<IdentityModuleOptions>()!;
        
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddApplication(Application.AssemblyReference.Assembly);
        services.AddInfrastructure(options);
        
        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IdentityModuleOptions moduleOptions)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(moduleOptions.Database.ConnectionString, builder =>
            {
                builder.MigrationsHistoryTable(moduleOptions.Database.MigrationsHistoryTable);
                builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityDbContext>());

        return services;
    }
}

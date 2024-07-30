using Dapper;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Users;
using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.Companies;
using Mastery.Modules.Career.Domain.Jobs;
using Mastery.Modules.Career.Domain.Users;
using Mastery.Modules.Career.Infrastructure.Authentication;
using Mastery.Modules.Career.Infrastructure.Persistence;
using Mastery.Modules.Career.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Career.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder =>
            {
                builder.MigrationsHistoryTable("ef_migrations_history");
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}

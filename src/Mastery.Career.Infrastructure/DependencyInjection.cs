using Dapper;
using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Domain.Categories;
using Mastery.Career.Domain.Companies;
using Mastery.Career.Domain.Jobs;
using Mastery.Career.Domain.Users;
using Mastery.Career.Infrastructure.Persistence;
using Mastery.Career.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Career.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder =>
            {
                builder.MigrationsHistoryTable("ef_migrations_history");
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

        return services;
    }
}

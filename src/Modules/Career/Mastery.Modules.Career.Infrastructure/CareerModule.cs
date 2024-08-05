using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.Companies;
using Mastery.Modules.Career.Domain.Jobs;
using Mastery.Modules.Career.Infrastructure.Persistence;
using Mastery.Modules.Career.Infrastructure.Persistence.Repositories;
using Mastery.Modules.Career.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Career.Infrastructure;

public static class CareerModule
{
    public static IServiceCollection AddCareerModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<CareerDbContext>(options =>
        {
            options.UseNpgsql(connectionString, builder =>
            {
                builder.MigrationsHistoryTable("ef_migrations_history");
                builder.MigrationsAssembly(typeof(CareerDbContext).Assembly.FullName);
            });

            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobRepository, JobRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CareerDbContext>());

        return services;
    }
}

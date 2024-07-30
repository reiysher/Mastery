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
using Mastery.Modules.Career.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using FluentValidation;

namespace Mastery.Modules.Career.Infrastructure;

public static class CareerModule
{
    public static IServiceCollection AddCareerModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

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
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CareerDbContext>());

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }

    public static IApplicationBuilder MapCareerModuleEndpoints(this WebApplication app)
    {
        app.MapEndpoints();

        return app;
    }
}

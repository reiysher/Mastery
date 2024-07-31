using Dapper;
using Mastery.Common.Application.Data;
using Mastery.Common.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Mastery.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}

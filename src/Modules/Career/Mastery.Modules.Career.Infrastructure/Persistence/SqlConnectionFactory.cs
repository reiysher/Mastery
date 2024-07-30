using Mastery.Modules.Career.Application.Abstractions.Data;
using Npgsql;
using System.Data;

namespace Mastery.Modules.Career.Infrastructure.Persistence;

internal sealed class SqlConnectionFactory(NpgsqlDataSource dataSource) : ISqlConnectionFactory
{
    public async ValueTask<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await dataSource.OpenConnectionAsync(cancellationToken);
    }
}

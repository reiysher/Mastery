using Mastery.Common.Application.Data;
using Npgsql;
using System.Data;

namespace Mastery.Common.Infrastructure.Data;

internal sealed class SqlConnectionFactory(NpgsqlDataSource dataSource) : ISqlConnectionFactory
{
    public async ValueTask<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await dataSource.OpenConnectionAsync(cancellationToken);
    }
}

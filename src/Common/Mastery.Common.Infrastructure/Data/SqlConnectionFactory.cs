using System.Data.Common;
using Mastery.Common.Application.Data;
using Npgsql;

namespace Mastery.Common.Infrastructure.Data;

internal sealed class SqlConnectionFactory(NpgsqlDataSource dataSource) : ISqlConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await dataSource.OpenConnectionAsync(cancellationToken);
    }
}

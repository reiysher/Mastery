using System.Data;

namespace Mastery.Common.Application.Data;

public interface ISqlConnectionFactory
{
    ValueTask<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}

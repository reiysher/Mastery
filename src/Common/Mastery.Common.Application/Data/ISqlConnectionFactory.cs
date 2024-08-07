using System.Data.Common;

namespace Mastery.Common.Application.Data;

public interface ISqlConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}

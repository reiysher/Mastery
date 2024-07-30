using System.Data;

namespace Mastery.Modules.Career.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}

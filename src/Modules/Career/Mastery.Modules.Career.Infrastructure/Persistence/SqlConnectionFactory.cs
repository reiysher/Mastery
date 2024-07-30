using Mastery.Modules.Career.Application.Abstractions.Data;
using Npgsql;
using System.Data;

namespace Mastery.Modules.Career.Infrastructure.Persistence;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    private readonly string connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var conection = new NpgsqlConnection(connectionString);
        conection.Open();

        return conection;
    }
}

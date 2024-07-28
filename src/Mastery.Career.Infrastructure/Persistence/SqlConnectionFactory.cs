using Mastery.Career.Application.Abstractions.Data;
using Npgsql;
using System.Data;

namespace Mastery.Career.Infrastructure.Persistence;

internal class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    private readonly string connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var conection = new NpgsqlConnection(connectionString);
        conection.Open();

        return conection;
    }
}

using Npgsql;
using Weasel.Postgresql.Connections;

namespace Nutrition.Infrastructure;

internal class CustomNpgSqlDataSourceFactory : INpgsqlDataSourceFactory
{
    public NpgsqlDataSource Create(string connectionString) => NpgsqlDataSource.Create(new NpgsqlConnectionStringBuilder(connectionString) { Enlist = true });
}

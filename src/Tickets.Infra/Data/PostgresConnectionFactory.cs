using System.Data;
using Npgsql;

namespace Tickets.Infra.Data
{
    public class PostgresConnectionFactory : IConnectionFactory
    {
        private const string POSTGRES_CONNECTION = "Server=localhost;Port=5432;Database=tickets_db;User Id=postgres;Password=@psqlpass;";

        public IDbConnection CreateConnection() =>
            new NpgsqlConnection(POSTGRES_CONNECTION);
    }
}
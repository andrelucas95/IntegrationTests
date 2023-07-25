using System.Data;
using Npgsql;

namespace Tickets.Infra.Data
{
    public class PostgresConnectionFactory : IConnectionFactory
    {
        //TODO: Use IConfiguration get from environment
        private const string POSTGRES_CONNECTION = "Server=ticketsdb;Port=5432;Database=tickets_db;User Id=postgres;Password=@psqlpass;";

        public IDbConnection CreateConnection() =>
            new NpgsqlConnection(POSTGRES_CONNECTION);
    }
}
using System.Data;
using System.Data.SqlClient;

namespace Tickets.Infra.Data
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=TICKETS_DB;Integrated Security=True";
        public IDbConnection CreateConnection() =>
            new SqlConnection(_connectionString);
    }
}
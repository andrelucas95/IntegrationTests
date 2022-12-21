using System.Data;
using System.Data.SQLite;

namespace Tickets.Infra.Data
{
    public class InMemoryConnectionFactory : IConnectionFactory
    {
        private readonly SQLiteConnection _cnn;

        public InMemoryConnectionFactory()
        {
            _cnn = new SQLiteConnection("DataSource=:memory:;Version=3;New=True;");
        }

        public IDbConnection CreateConnection() => _cnn;
    }
}
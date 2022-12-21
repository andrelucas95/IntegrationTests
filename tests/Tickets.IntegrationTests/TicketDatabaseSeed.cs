using System.Data;
using System.Data.SQLite;

namespace Tickets.IntegrationTests
{
    public class TicketDatabaseSeed
    {
        private readonly object _lockOpenConnection = new();
        public void EnsureDatabase(IDbConnection connection)
        {
            var cnn = (SQLiteConnection) connection;
            lock (_lockOpenConnection)
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
            }

            string sql = @"CREATE TABLE Tickets(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Title TEXT,
                            Description TEXT,
                            Priority INTEGER,
                            OpenedAt TEXT,
                            FinishedAt TEXT,
                            CanceledAt TEXT,
                            Reference TEXT)";

            SQLiteCommand command = new(sql, cnn);
            command.ExecuteNonQuery();

            using (command = cnn.CreateCommand())
            {
                command.CommandText = @"INSERT into Tickets(Id, Title, Description, OpenedAt, Reference)
                                        values
                                      (@Id,@Title,@Description,@OpenedAt,@Reference)";
                command.Prepare();
                command.Parameters.AddWithValue("@Id", 1);
                command.Parameters.AddWithValue("@Title", "Blablabla Loremlorem");
                command.Parameters.AddWithValue("@Description", "Ipsum dolor ipsum");
                command.Parameters.AddWithValue("@OpenedAt", DateTime.Now);
                command.Parameters.AddWithValue("@Reference", Guid.NewGuid().ToString());
                command.ExecuteNonQuery();
            }
        }
    }
}
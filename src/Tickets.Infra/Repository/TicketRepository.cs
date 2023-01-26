using System.Data;
using Dapper;
using Tickets.Domain.Entity;
using Tickets.Domain.Repository;
using Tickets.Infra.Data;

namespace Tickets.Infra.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IConnectionFactory _connFactory;
        private readonly IDbConnection _connection;

        public TicketRepository(IConnectionFactory connFactory)
        {
            _connFactory = connFactory;
            _connection = _connFactory.CreateConnection();
        }

        public async Task Insert(Ticket ticket)
        {
            var parameter = new
            {
                ticket.Title,
                ticket.Description,
                ticket.Priority,
                OpenedAt = DateTime.Now,
                ticket.Reference
            };

            await _connection.ExecuteAsync(
                @"INSERT INTO Tickets
                (Title, Description, Priority, OpenedAt, Reference)
                VALUES
                (@Title, @Description, @Priority, @OpenedAt, @Reference);", parameter);
        }

        public async Task<IEnumerable<Ticket>> List()
        {
            return await _connection
                .QueryAsync<Ticket>(@"SELECT * FROM Tickets ORDER BY OpenedAt DESC;");
        }
    }
}
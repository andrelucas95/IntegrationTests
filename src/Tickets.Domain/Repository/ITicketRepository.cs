using Tickets.Domain.Entity;

namespace Tickets.Domain.Repository
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> List();
        Task Insert(Ticket ticket);
    }
}
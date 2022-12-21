using Tickets.Domain.Enum;

namespace Tickets.Domain.Entity
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public TicketPriorityType Priority { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? CanceledAt { get; set; }
    }
}
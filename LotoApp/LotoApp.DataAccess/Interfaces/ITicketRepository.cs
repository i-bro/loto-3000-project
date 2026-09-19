using LotoApp.Domain.Models;

namespace LotoApp.DataAccess.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userid);
        Task<IEnumerable<Ticket>> GetTicketsForActiveSessionAsync(int sessionId);
    }
}

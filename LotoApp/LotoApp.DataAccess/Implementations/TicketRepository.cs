using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoApp.DataAccess.Implementations
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(LotoAppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userid)
        {
            return await _context.Tickets
                .Include(s => s.Session)
                .Where(t => t.UserId == userid)
                .OrderByDescending(t => t.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsForActiveSessionAsync(int sessionId)
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Where(t => t.SessionId == sessionId)
                .ToListAsync();
        }
    }
}

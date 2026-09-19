using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoApp.DataAccess.Implementations
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(LotoAppDbContext context) : base(context)
        {
        }
        public async Task<Session?> GetActiveSessionAsync()
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
        }

        public async Task<Session?> GetActiveSessionWithTicketsAsync()
        {
            return await _context.Sessions.Include(s => s.Tickets)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(s => s.IsActive);
        }
    }
}

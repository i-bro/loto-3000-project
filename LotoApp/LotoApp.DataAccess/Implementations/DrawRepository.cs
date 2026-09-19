using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoApp.DataAccess.Implementations
{
    public class DrawRepository : Repository<Draw>, IDrawRepository
    {
        public DrawRepository(LotoAppDbContext contest) : base(contest)
        {

        }
        public async Task<IEnumerable<Draw>> GetDrawHistoryAsync()
        {
            return await _context.Draws
                .Include(d => d.Admin)
                .OrderByDescending(d => d.DrawnAt)
                .ToListAsync();
        }

        public async Task<Draw> GetLatestDrawAsync()
        {
            return await _context.Draws
                .Include(d => d.Admin)
                .OrderByDescending(d => d.DrawnAt)
                .FirstOrDefaultAsync();
        }
    }
}

using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoApp.DataAccess.Implementations
{
    public class WinnerRepository : Repository<Winner>, IWinnerRepository
    {
        public WinnerRepository(LotoAppDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Winner>> GetWinnersBoardAsync()
        {
            return await _context.Winners
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Winner>> GetWinnersByDrawIdAsync(int drawId)
        {
            return await _context.Winners
                .Where(w => w.DrawId == drawId)
                .OrderByDescending(w => w.MatchedCount)
                .ToListAsync();
        }
    }
}

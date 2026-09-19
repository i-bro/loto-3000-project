using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LotoApp.DataAccess.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //private readonly LotoAppDbContext _context;
        public UserRepository(LotoAppDbContext context) : base(context)
        {
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}

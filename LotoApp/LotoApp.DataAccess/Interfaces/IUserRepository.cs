using LotoApp.Domain.Models;

namespace LotoApp.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
    }
}

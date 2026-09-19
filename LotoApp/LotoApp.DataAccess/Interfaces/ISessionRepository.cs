using LotoApp.Domain.Models;

namespace LotoApp.DataAccess.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<Session?> GetActiveSessionAsync();
        Task<Session?> GetActiveSessionWithTicketsAsync();
    }
}

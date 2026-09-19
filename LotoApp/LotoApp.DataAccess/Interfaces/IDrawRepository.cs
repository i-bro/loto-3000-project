using LotoApp.Domain.Models;

namespace LotoApp.DataAccess.Interfaces
{
    public interface IDrawRepository : IRepository<Draw>
    {
        Task<Draw> GetLatestDrawAsync();
        Task<IEnumerable<Draw>> GetDrawHistoryAsync();
    }
}

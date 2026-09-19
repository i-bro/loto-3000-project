using LotoApp.Domain.Models;

namespace LotoApp.DataAccess.Interfaces
{
    public interface IWinnerRepository : IRepository<Winner> 
    {
        Task<IEnumerable<Winner>> GetWinnersBoardAsync();
        Task<IEnumerable<Winner>> GetWinnersByDrawIdAsync(int drawId);
    }
}

using LotoApp.DTOs;

namespace LotoApp.Services.Interfaces
{
    public interface IWinnerService
    {
        public Task<List<WinnerBoardDto>> GetWinnersBoardAsync();
    }
}

using LotoApp.DataAccess.Interfaces;
using LotoApp.DTOs;
using LotoApp.Services.Interfaces;

namespace LotoApp.Services.Implementation
{
    public class WinnerService : IWinnerService
    {
        private readonly IWinnerRepository _winnerReposiory;

        public WinnerService(IWinnerRepository winnderRepository)
        {
            _winnerReposiory = winnderRepository;
        }
        public async Task<List<WinnerBoardDto>> GetWinnersBoardAsync()
        {
            var winners = await _winnerReposiory.GetAllAsync();

            return winners.OrderByDescending(w => w.CreatedAt)
                .Select(w => new WinnerBoardDto
                {
                    PlayerFullName = w.PlayerFullName,
                    TicketNumbers = w.TicketNumbers,
                    PrizeWon = w.PrizeWon.ToString(),
                    WonAt = w.CreatedAt
                }).ToList();
        }
    }
}

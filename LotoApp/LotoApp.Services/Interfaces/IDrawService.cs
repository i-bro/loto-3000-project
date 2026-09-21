using LotoApp.DTOs;

namespace LotoApp.Services.Interfaces
{
    public interface IDrawService
    {
        Task<DrawResultDto> ExecuteDrawAsync(int adminId);
        Task<DrawResultDto> GetLatestDrawAsync();
    }
}

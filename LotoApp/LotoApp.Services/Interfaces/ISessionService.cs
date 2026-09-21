namespace LotoApp.Services.Interfaces
{
    public interface ISessionService
    {
        Task<int> GetActiveSessionIdAsync();
    }
}

using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.Services.Interfaces;

namespace LotoApp.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
        public async Task<int> GetActiveSessionIdAsync()
        {
            var activeSession = await _sessionRepository.GetActiveSessionAsync();

            if(activeSession == null)
            {
                activeSession = new Session
                {
                    StartedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _sessionRepository.AddAsync(activeSession);
                await _sessionRepository.SaveChangesAsync();
            }

            return activeSession.Id;
        }
    }
}

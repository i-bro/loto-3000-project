using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs;
using LotoApp.Services.Interfaces;
using System.Security.Cryptography;

namespace LotoApp.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IDrawRepository _drawRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITicketRepository _ticketRepository;

        public DrawService(IDrawRepository drawRepository, ISessionRepository sessionRepository, ITicketRepository ticketRepository)
        {
            _drawRepository = drawRepository;
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
        }
        public async Task<DrawResultDto> ExecuteDrawAsync(int adminId)
        {
            var activeSession = await _sessionRepository.GetActiveSessionAsync();

            if(activeSession == null)
            {
                throw new NullReferenceException("There is no active sessions");
            }

            var winningNumbers = GenerateWinningNumbers();

            var draw = new Draw{
                SessionId = activeSession.Id,
                AdminId = adminId,
                DrawnNumbers = winningNumbers,
                DrawnAt = DateTime.UtcNow
            };

            await _drawRepository.AddAsync(draw);

            var sesionTickets = await _ticketRepository.GetTicketsForActiveSessionAsync(activeSession.Id);

            var winningTicketsSummary = new List<WinnerSummaryDto>();

            var matchDistribution = new Dictionary<int, int>
            {
                { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }
            };

            foreach(var ticket in sesionTickets)
            {
                var ticketNumbers = ticket.Numbers;
                int matchedCount = ticketNumbers.Intersect(winningNumbers).Count();

                if(matchedCount >= 3)
                {
                    matchDistribution[matchedCount]++;

                    winningTicketsSummary.Add(new WinnerSummaryDto
                    {
                        TicketId = ticket.Id,
                        UserId = ticket.UserId,
                        Numbers = ticketNumbers,
                        MatchedCount = matchedCount
                    });
                }
            }

            activeSession.IsActive = false;
            _sessionRepository.Update(activeSession);

            var newSession = new Session
            {
                StartedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _drawRepository.SaveChangesAsync();

            return new DrawResultDto
            {
                DrawId = draw.Id,
                SessionId = draw.SessionId,
                WinningNumbers = winningNumbers,
                ExecutedAt = draw.DrawnAt,
                TotalTicketsEvaluated = sesionTickets.Count(),
                MatchCounts = matchDistribution,
                WinningTickets = winningTicketsSummary
            };
        }

        public async Task<DrawResultDto> GetLatestDrawAsync()
        {
            var allDraws = await _drawRepository.GetAllAsync();
            var latestDraw = allDraws.OrderByDescending(d => d.DrawnAt).FirstOrDefault();
            if(latestDraw == null)
            {
                return null;
            }

            var winningNumbers = latestDraw.DrawnNumbers.ToList();

            return new DrawResultDto
            {
                DrawId = latestDraw.Id,
                SessionId = latestDraw.SessionId,
                WinningNumbers = latestDraw.DrawnNumbers,
                ExecutedAt = latestDraw.DrawnAt
            };

        }

        private List<int> GenerateWinningNumbers()
        {
            var numbers = new HashSet<int>();

            while(numbers.Count < 7)
            {
                int number = RandomNumberGenerator.GetInt32(1, 38);
                numbers.Add(number);
            }

            return numbers.OrderBy(n => n).ToList();
        }
    }
}

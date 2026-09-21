using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs;
using LotoApp.Services.Interfaces;

namespace LotoApp.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionRepository _sessionRepository;

        public TicketService(ITicketRepository ticketRepository, ISessionRepository sessionRepository)
        {
            _ticketRepository = ticketRepository;
            _sessionRepository = sessionRepository;
        }
        public async Task<IEnumerable<TicketResponseDto>> GetUserTicketAsync(int userId)
        {
            var allTickets = await _ticketRepository.GetAllAsync();

            return allTickets.Where(t => t.UserId == userId)
                .Select(t => new TicketResponseDto
                {
                    Id = t.Id,
                    SessionId = t.SessionId,
                    Numbers = t.Numbers,
                    CreatedAt = t.SubmittedAt
                });
        }

        public async Task<TicketResponseDto> SubmitTicketAsync(SubmitTicketDto dto, int userId)
        {
            if(dto == null || dto.Numbers == null)
            {
                throw new ArgumentNullException("Ticket cannot be empty");
            }
            if(dto.Numbers.Count != 7)
            {
                throw new ArgumentException("A valid ticket must contain exactly 7 numbers");
            }
            if(dto.Numbers.Any(n => n < 1 || n > 37))
            {
                throw new ArgumentException("All numbers must be between 1 and 37");
            }
            if(dto.Numbers.Distinct().Count() != 7)
            {
                throw new ArgumentException("Duplicate numbers are not allowed on a single ticket");
            }

            var activeSession = await _sessionRepository.GetActiveSessionAsync();

            if(activeSession == null)
            {
                throw new NullReferenceException("There is no curenly active lotary session");
            }

            var sortedNumbers = dto.Numbers.OrderBy(n => n).ToList();
            var numbersString = string.Join(",", sortedNumbers);

            var ticket = new Ticket
            {
                UserId = userId,
                SessionId = activeSession.Id,
                Numbers = sortedNumbers,
                SubmittedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return new TicketResponseDto
            {
                Id = ticket.Id,
                SessionId = ticket.SessionId,
                Numbers = sortedNumbers,
                CreatedAt = ticket.SubmittedAt
            };
        }
    }
}

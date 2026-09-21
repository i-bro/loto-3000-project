using LotoApp.DTOs;

namespace LotoApp.Services.Interfaces
{
     public interface ITicketService
    {
        Task<TicketResponseDto> SubmitTicketAsync(SubmitTicketDto dto, int userId);
        Task<IEnumerable<TicketResponseDto>> GetUserTicketAsync(int userId);
    }
}

namespace LotoApp.DTOs
{
    public class WinnerResponseDto
    {
        public int WinnerId {get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int DrawId { get; set; }
        public int MatchedCount {get; set;}
        public string? Prize {get; set;}
        public DateTime WonAt {get; set;}
    }
}

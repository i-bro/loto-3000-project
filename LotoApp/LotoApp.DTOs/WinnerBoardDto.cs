namespace LotoApp.DTOs
{
    public class WinnerBoardDto
    {
        public string PlayerFullName {get; set; }
        public List<int> TicketNumbers { get; set; }
        public string? PrizeWon { get; set; }
        public DateTime WonAt {get; set;}
    }
}

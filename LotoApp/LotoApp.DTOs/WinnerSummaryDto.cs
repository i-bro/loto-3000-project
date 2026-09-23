namespace LotoApp.DTOs
{
    public class WinnerSummaryDto
    {
        public int TicketId {get; set;}
        public int UserId {get; set;}
        public List<int> Numbers {get; set;} = new List<int>();
        public string PlayerFullName {get; set;}
        public int MatchedCount {get; set;}
        public string? Prize { get; set; }
        public DateTime WonAt { get; set; }
    }
}

namespace LotoApp.DTOs
{
    public class DrawResultDto
    {
        public int DrawId {get; set;}
        public int SessionId {get; set;}
        public List<int> WinningNumbers {get; set;} = new List<int>();
        public DateTime ExecutedAt {get; set; }
        public int TotalTicketsEvaluated { get; set; }
        public Dictionary<int, int> MatchCounts {get; set;} = new Dictionary<int, int>();
        public List<WinnerSummaryDto> WinningTickets {get; set;} = new List<WinnerSummaryDto>();
    }
}

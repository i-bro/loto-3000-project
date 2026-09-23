namespace LotoApp.DTOs
{
    public class TicketResponseDto
    {
        public int Id {get; set;}
        public int SessionId {get; set;}
        public List<int> Numbers {get; set;} = new List<int>();
        public DateTime CreatedAt {get; set; }

        public string Message { get; set; } = "Please wait for the draw. If you win a prize, your name will appear on the winners board!";
        public string WinnersBoardUrl {get; set;} = "/api/winners";
    }
}

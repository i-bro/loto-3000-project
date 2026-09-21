namespace LotoApp.DTOs
{
    public class TicketResponseDto
    {
        public int Id {get; set;}
        public int SessionId {get; set;}
        public List<int> Numbers {get; set;} = new List<int>();
        public DateTime CreatedAt {get; set;}
    }
}

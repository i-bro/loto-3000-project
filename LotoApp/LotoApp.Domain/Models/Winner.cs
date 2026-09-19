using LotoApp.Domain.Enums;

namespace LotoApp.Domain.Models
{
    public class Winner : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } 

        public int DrawId { get; set; }
        public Draw Draw { get; set; }

        public string PlayerFullName { get; set; }
        public List<int> TicketNumbers { get; set; } = new();
        public int MatchedCount { get; set; }
        public PrizeEnum PrizeWon { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

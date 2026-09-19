using static System.Collections.Specialized.BitVector32;

namespace LotoApp.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public List<int> Numbers { get; set; } = new();
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}

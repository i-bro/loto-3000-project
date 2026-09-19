namespace LotoApp.Domain.Models
{
    public class Session : BaseEntity
    {
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EndedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}

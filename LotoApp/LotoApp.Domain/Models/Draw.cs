namespace LotoApp.Domain.Models
{
    public class Draw : BaseEntity
    {
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public List<int> DrawnNumbers { get; set; } = new();
        public DateTime DrawnAt { get; set; } = DateTime.UtcNow;

        public int AdminId { get; set; }
        public User Admin { get; set; } = null!;
    }
}

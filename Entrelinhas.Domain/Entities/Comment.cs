namespace Entrelinhas.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid CycleId { get; set; }
        public string Chapter { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public Cycle Cycle { get; set; } = null!;
    }
}

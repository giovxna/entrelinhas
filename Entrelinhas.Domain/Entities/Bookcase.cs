using Entrelinhas.Domain.Enums;

namespace Entrelinhas.Domain.Entities
{
    public class Bookcase
    {
        public Guid BookcaseId { get; set; }
        public BookcaseStatus Status { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}

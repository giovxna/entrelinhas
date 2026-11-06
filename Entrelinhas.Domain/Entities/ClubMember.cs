using Entrelinhas.Domain.Enums;

namespace Entrelinhas.Domain.Entities
{
    public class ClubMember
    {
        public Guid ClubMemberId { get; set; }
        public Guid UserId { get; set; }
        public Guid ClubId { get; set; }
        public ClubRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public Club Club { get; set; } = null!;
    }
}

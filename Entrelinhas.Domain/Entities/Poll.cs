using Entrelinhas.Domain.Enums;

namespace Entrelinhas.Domain.Entities
{
    public class Poll
    {
        public Guid PollId { get; set; }
        public DateTime ClosesAt { get; set; }
        public PollType Type { get; set; }
        public Guid ClubId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Club Club { get; set; } = null!;
        public ICollection<PollOption> Options { get; set; } = new List<PollOption>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}

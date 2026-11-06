namespace Entrelinhas.Domain.Entities
{
    public class Vote
    {
        public Guid VoteId { get; set; }
        public Guid PollId { get; set; }
        public Guid PollOptionId { get; set; }
        public Guid UserId { get; set; }

        public Poll Poll { get; set; } = null!;
        public PollOption PollOption { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}

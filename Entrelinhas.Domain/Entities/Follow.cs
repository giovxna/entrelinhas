namespace Entrelinhas.Domain.Entities
{
    public class Follow
    {
        public Guid FollowId { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FollowedId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Follower { get; set; } = null!;
        public User Followed { get; set; } = null!;
    }
}

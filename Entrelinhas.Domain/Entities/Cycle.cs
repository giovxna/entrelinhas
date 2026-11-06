namespace Entrelinhas.Domain.Entities
{
    public class Cycle
    {
        public Guid CycleId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly Due { get; set; }
        public Guid ClubId { get; set; }
        public Guid BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Club Club { get; set; } = null!;
        public Book Book { get; set; } = null!;
        public ICollection<CycleParticipant> Participants { get; set; } = new List<CycleParticipant>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

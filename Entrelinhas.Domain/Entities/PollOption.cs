namespace Entrelinhas.Domain.Entities
{
    public class PollOption
    {
        public Guid PollOptionId { get; set; }
        public Guid PollId { get; set; }
        public Guid? BookId { get; set; }
        public int? Deadline { get; set; }

        public Poll Poll { get; set; } = null!;
        public Book? Book { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}

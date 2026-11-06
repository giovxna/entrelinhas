namespace Entrelinhas.Domain.Entities
{
    public class Club
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<ClubMember> Members { get; set; } = new List<ClubMember>();
        public ICollection<Cycle> Cycles { get; set; } = new List<Cycle>();
        public ICollection<Poll> Polls { get; set; } = new List<Poll>();
    }
}

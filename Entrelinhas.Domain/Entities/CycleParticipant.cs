using Entrelinhas.Domain.Enums;

namespace Entrelinhas.Domain.Entities
{
    public class CycleParticipant
    {
        public Guid CycleParticipantId { get; set; }
        public CycleParticipantStatus Status { get; set; }
        public string? Reason { get; set; }
        public Guid ClubMemberId { get; set; }
        public Guid CycleId { get; set; }

        public ClubMember ClubMember { get; set; } = null!;
        public Cycle Cycle { get; set; } = null!;
    }
}

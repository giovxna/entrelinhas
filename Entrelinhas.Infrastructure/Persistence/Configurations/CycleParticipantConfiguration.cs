using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class CycleParticipantConfiguration : IEntityTypeConfiguration<CycleParticipant>
    {
        public void Configure(EntityTypeBuilder<CycleParticipant> builder)
        {
            builder.ToTable("cycle_participant");
            builder.HasKey(p => p.CycleParticipantId);

            builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(255).IsRequired();

            builder.HasIndex(p => new { p.ClubMemberId, p.CycleId }).IsUnique();

            builder.HasOne(p => p.ClubMember)
                .WithMany()
                .HasForeignKey(p => p.ClubMemberId)
                .HasConstraintName("fk_cycle_participant_club_member_id_club_member")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cycle)
                .WithMany(c => c.Participants)
                .HasForeignKey(p => p.CycleId)
                .HasConstraintName("fk_cycle_participant_cycle_id_cycle")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

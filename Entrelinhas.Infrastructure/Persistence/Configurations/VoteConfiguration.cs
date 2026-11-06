using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("vote");
            builder.HasKey(v => v.VoteId);

            builder.HasIndex(v => new { v.PollId, v.UserId }).IsUnique();

            builder.HasOne(v => v.Poll)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PollId)
                .HasConstraintName("fk_vote_poll_id_poll")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.PollOption)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => new { v.PollOptionId, v.PollId })
                .HasPrincipalKey(o => new { o.PollOptionId, o.PollId })
                .HasConstraintName("fk_vote_poll_option_id_poll_option")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId)
                .HasConstraintName("fk_vote_user_id_users")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

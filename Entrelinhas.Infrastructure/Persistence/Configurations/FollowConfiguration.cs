using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.ToTable("follower", t =>
                t.HasCheckConstraint("ck_follower_not_self", "follower_id <> followed_id"));
            builder.HasKey(f => f.FollowId);

            builder.HasIndex(f => new { f.FollowerId, f.FollowedId }).IsUnique();

            builder.HasOne(f => f.Follower)
                .WithMany()
                .HasForeignKey(f => f.FollowerId)
                .HasConstraintName("fk_follower_follower_id_users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Followed)
                .WithMany()
                .HasForeignKey(f => f.FollowedId)
                .HasConstraintName("fk_follower_followed_id_users")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comment");
            builder.HasKey(c => c.CommentId);

            builder.Property(c => c.Chapter).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Text).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("fk_comment_user_id_users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Cycle)
                .WithMany(cycle => cycle.Comments)
                .HasForeignKey(c => c.CycleId)
                .HasConstraintName("fk_comment_cycle_id_cycle")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class PollOptionConfiguration : IEntityTypeConfiguration<PollOption>
    {
        public void Configure(EntityTypeBuilder<PollOption> builder)
        {
            builder.ToTable("poll_option", t =>
                t.HasCheckConstraint("ck_poll_option_book_xor_deadline", "(book_id IS NULL) <> (deadline IS NULL)"));
            builder.HasKey(o => o.PollOptionId);

            builder.HasAlternateKey(o => new { o.PollOptionId, o.PollId })
                .HasName("ak_poll_option_poll_option_id_poll_id");

            builder.HasIndex(o => new { o.PollId, o.BookId }).IsUnique();
            builder.HasIndex(o => new { o.PollId, o.Deadline }).IsUnique();

            builder.HasOne(o => o.Poll)
                .WithMany(p => p.Options)
                .HasForeignKey(o => o.PollId)
                .HasConstraintName("fk_poll_option_poll_id_poll")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Book)
                .WithMany()
                .HasForeignKey(o => o.BookId)
                .HasConstraintName("fk_poll_option_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

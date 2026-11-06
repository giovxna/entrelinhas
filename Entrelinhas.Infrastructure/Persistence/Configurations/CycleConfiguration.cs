using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class CycleConfiguration : IEntityTypeConfiguration<Cycle>
    {
        public void Configure(EntityTypeBuilder<Cycle> builder)
        {
            builder.ToTable("cycle", t =>
                t.HasCheckConstraint("ck_cycle_due_after_start", "due >= start_date"));
            builder.HasKey(c => c.CycleId);

            builder.HasOne(c => c.Club)
                .WithMany(club => club.Cycles)
                .HasForeignKey(c => c.ClubId)
                .HasConstraintName("fk_cycle_club_id_club")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Book)
                .WithMany()
                .HasForeignKey(c => c.BookId)
                .HasConstraintName("fk_cycle_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.ToTable("poll");
            builder.HasKey(p => p.PollId);

            builder.Property(p => p.Type).HasConversion<string>().HasMaxLength(255).IsRequired();

            builder.HasOne(p => p.Club)
                .WithMany(c => c.Polls)
                .HasForeignKey(p => p.ClubId)
                .HasConstraintName("fk_poll_club_id_club")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

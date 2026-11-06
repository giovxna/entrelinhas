using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class ClubMemberConfiguration : IEntityTypeConfiguration<ClubMember>
    {
        public void Configure(EntityTypeBuilder<ClubMember> builder)
        {
            builder.ToTable("club_member");
            builder.HasKey(m => m.ClubMemberId);

            builder.Property(m => m.Role).HasConversion<string>().HasMaxLength(255).IsRequired();

            builder.HasIndex(m => new { m.UserId, m.ClubId }).IsUnique();

            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .HasConstraintName("fk_club_member_user_id_users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Club)
                .WithMany(c => c.Members)
                .HasForeignKey(m => m.ClubId)
                .HasConstraintName("fk_club_member_club_id_club")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

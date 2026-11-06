using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class BookcaseConfiguration : IEntityTypeConfiguration<Bookcase>
    {
        public void Configure(EntityTypeBuilder<Bookcase> builder)
        {
            builder.ToTable("bookcase");
            builder.HasKey(b => b.BookcaseId);

            builder.Property(b => b.Status).HasConversion<string>().HasMaxLength(255).IsRequired();

            builder.HasIndex(b => new { b.UserId, b.BookId }).IsUnique();

            builder.HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .HasConstraintName("fk_bookcase_user_id_users")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId)
                .HasConstraintName("fk_bookcase_book_id_book")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

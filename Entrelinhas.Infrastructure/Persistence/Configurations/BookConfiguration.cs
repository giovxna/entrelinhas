using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entrelinhas.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("book");
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title).HasMaxLength(255).IsRequired();
            builder.Property(b => b.Author).HasMaxLength(255);
            builder.Property(b => b.ApiId).HasMaxLength(255).IsRequired();

            builder.HasIndex(b => b.ApiId).IsUnique();
        }
    }
}

using Entrelinhas.Domain.Entities;

namespace Entrelinhas.Tests.Domain
{
    public class UserTests
    {
        private static User CreateUser() => new()
        {
            UserId = Guid.NewGuid(),
            Name = "Maria",
            Email = "maria@example.com",
            PasswordHash = "hash",
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        [Fact]
        public void Anonymize_RemovesPersonalData()
        {
            var user = CreateUser();
            var now = new DateTime(2026, 11, 5, 12, 0, 0, DateTimeKind.Utc);

            user.Anonymize(now);

            Assert.Equal(User.AnonymizedName, user.Name);
            Assert.Equal($"deleted-{user.UserId}@entrelinhas.invalid", user.Email);
            Assert.Empty(user.PasswordHash);
            Assert.Equal(now, user.DeletedAt);
            Assert.True(user.IsDeleted);
        }

        [Fact]
        public void Anonymize_IsIdempotent()
        {
            var user = CreateUser();
            var first = new DateTime(2026, 11, 5, 12, 0, 0, DateTimeKind.Utc);

            user.Anonymize(first);
            user.Anonymize(first.AddDays(1));

            Assert.Equal(first, user.DeletedAt);
        }
    }
}

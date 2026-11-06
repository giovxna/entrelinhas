using System.Text.RegularExpressions;
using Entrelinhas.Domain.Entities;
using Entrelinhas.Infrastructure;
using Entrelinhas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Entrelinhas.Tests.Infrastructure
{
    public sealed class DbModelTests : IDisposable
    {
        private readonly ServiceProvider _provider;
        private readonly IServiceScope _scope;
        private readonly EntrelinhasDbContext _context;

        public DbModelTests()
        {
            _provider = new ServiceCollection()
                .AddInfrastructure("Host=localhost;Database=entrelinhas_model_tests")
                .BuildServiceProvider();
            _scope = _provider.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<EntrelinhasDbContext>();
        }

        private IModel Model => _context.GetService<IDesignTimeModel>().Model;

        private IEnumerable<IForeignKey> AllForeignKeys =>
            Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

        [Fact]
        public void ForeignKeysToUsers_AreAllRestrict()
        {
            var userForeignKeys = Model.FindEntityType(typeof(User))!.GetReferencingForeignKeys().ToList();

            Assert.Equal(6, userForeignKeys.Count);
            Assert.All(userForeignKeys, fk => Assert.Equal(DeleteBehavior.Restrict, fk.DeleteBehavior));
        }

        [Fact]
        public void OnlyPollOptionToPoll_CascadesOnDelete()
        {
            var cascades = AllForeignKeys.Where(fk => fk.DeleteBehavior != DeleteBehavior.Restrict).ToList();

            var cascade = Assert.Single(cascades);
            Assert.Equal(DeleteBehavior.Cascade, cascade.DeleteBehavior);
            Assert.Equal(typeof(PollOption), cascade.DeclaringEntityType.ClrType);
            Assert.Equal(typeof(Poll), cascade.PrincipalEntityType.ClrType);
        }

        [Fact]
        public void AllForeignKeys_FollowDiagramNaming()
        {
            var foreignKeys = AllForeignKeys.ToList();

            Assert.Equal(18, foreignKeys.Count);
            Assert.All(foreignKeys, fk =>
            {
                var expected = $"fk_{fk.DeclaringEntityType.GetTableName()}_{fk.Properties[0].GetColumnName()}_{fk.PrincipalEntityType.GetTableName()}";
                Assert.Equal(expected, fk.GetConstraintName());
            });
        }

        [Fact]
        public void VoteToPollOption_IsCompositeWithPollId()
        {
            var fk = Model.FindEntityType(typeof(Vote))!.GetForeignKeys()
                .Single(f => f.PrincipalEntityType.ClrType == typeof(PollOption));

            Assert.Equal(
                new[] { nameof(Vote.PollOptionId), nameof(Vote.PollId) },
                fk.Properties.Select(p => p.Name));
            Assert.Equal(
                new[] { nameof(PollOption.PollOptionId), nameof(PollOption.PollId) },
                fk.PrincipalKey.Properties.Select(p => p.Name));
        }

        [Fact]
        public void Migrations_AreInSyncWithModel()
        {
            Assert.False(
                _context.Database.HasPendingModelChanges(),
                "O modelo mudou e não há migration. Rode: dotnet ef migrations add <Nome> -p Entrelinhas.Infrastructure -s entrelinhas.api -o Persistence/Migrations");
        }

        [Fact]
        public void GeneratedSql_HasSingleCascadeAndRestrictEverywhereElse()
        {
            var sql = _context.Database.GenerateCreateScript();

            Assert.Single(Regex.Matches(sql, "ON DELETE CASCADE"));
            Assert.Equal(17, Regex.Matches(sql, "ON DELETE RESTRICT").Count);
            Assert.Contains("CONSTRAINT fk_poll_option_poll_id_poll FOREIGN KEY (poll_id) REFERENCES poll (poll_id) ON DELETE CASCADE", sql);
        }

        public void Dispose()
        {
            _scope.Dispose();
            _provider.Dispose();
        }
    }
}

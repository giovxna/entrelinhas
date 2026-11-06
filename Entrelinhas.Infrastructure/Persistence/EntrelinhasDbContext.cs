using Entrelinhas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entrelinhas.Infrastructure.Persistence
{
    public class EntrelinhasDbContext(DbContextOptions<EntrelinhasDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Follow> Follows => Set<Follow>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Bookcase> Bookcases => Set<Bookcase>();
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<ClubMember> ClubMembers => Set<ClubMember>();
        public DbSet<Cycle> Cycles => Set<Cycle>();
        public DbSet<CycleParticipant> CycleParticipants => Set<CycleParticipant>();
        public DbSet<Poll> Polls => Set<Poll>();
        public DbSet<PollOption> PollOptions => Set<PollOption>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntrelinhasDbContext).Assembly);
        }
    }
}

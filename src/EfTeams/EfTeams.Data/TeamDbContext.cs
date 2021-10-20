using EfTeams.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EfTeams.Data
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options)
        { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamLeague> TeamLeagues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeamEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new CoachEntityConfiguration());
           // modelBuilder.ApplyConfiguration(new PlayerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeamLeagueEntityConfiguration());
        }
    }
}

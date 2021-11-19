using Bogus;
using EfTeams.Data;
using EfTeams.Data.Models;
using System.Linq;

namespace EfTeams.Tests.Builder
{
    public class PlayerBuilder
    {
        private readonly TeamDbContext _dbContext;

        public PlayerBuilder(TeamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Build()
        {
            _dbContext.SaveChanges();
        }

        public void AddPlayers(int count)
        {
            var playerFaker = new Faker<Player>().RuleFor(x => x.PlayerName, f => f.Name.FirstName())
                .RuleFor(x => x.Position, p => p.Random.Words(1))
                .RuleFor(x => x.Team, c => c.PickRandom<Team>(_dbContext.Teams.FirstOrDefault()));

            var players = playerFaker.Generate(count);
            _dbContext.AddRange(players);
        }

    }
}

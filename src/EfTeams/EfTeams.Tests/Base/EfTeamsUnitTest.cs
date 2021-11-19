using EfTeams.Business.Interfaces;
using EfTeams.Data;
using EfTeams.Repositories.Interfaces;
using EfTeams.Repositories.Repositories;
using EfTeams.Tests.Builder;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EfTeams.Tests.Base
{
    public abstract class EfTeamsUnitTest
    {
        public TeamDbContext db;
        public TeamDbContext dbSQL;

        //public ITeamsService TeamServices { get; set; }
        //public IPlayerRepository IPlayerRepository { get; set; }
        //public Mock<IPlayerRepository> IPlayerRepositoryMock { get; set; }
        public CoachBuilder coachBuilder { get; set; }
        public CountryBuilder countryBuilder { get; set; }
        public LeagueBuilder leagueBuilder { get; set; }
        public PlayerBuilder playerBuilder { get; set; }
        public TeamBuilder teamBuilder { get; set; }

        public TeamDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TeamDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new TeamDbContext(options);
        }

        public TeamDbContext GetSQLContext()
        {
            var options = new DbContextOptionsBuilder<TeamDbContext>()
            .UseSqlServer("Data Source=DESKTOP-411OTHH;Initial Catalog=EFTeamDB;Integrated Security=True")
            .Options;
            return new TeamDbContext(options);
        }

        [SetUp]
        public void Setup()
        {
            db = GetMemoryContext();
            dbSQL = GetSQLContext();
            coachBuilder = new CoachBuilder(db);
            countryBuilder = new CountryBuilder(db);
            leagueBuilder = new LeagueBuilder(db);
            teamBuilder = new TeamBuilder(db);
            playerBuilder = new PlayerBuilder(db);
            //IPlayerRepository = new PlayerRepository(db);

            if (db.Database.IsInMemory())
            {
                db.Database.EnsureDeleted();
                coachBuilder.AddCoaches(5);
                countryBuilder.AddCountries(5);
                leagueBuilder.AddLeagues(5);
                coachBuilder.Build();
                countryBuilder.Build();
                leagueBuilder.Build();

                teamBuilder.AddTeams(1);
                teamBuilder.Build();
                playerBuilder.AddPlayers(1);
                playerBuilder.Build();
                //playerBuilder.AddPlayers(5, coachBuilder, countryBuilder,leagueBuilder);

            }
        }
        
    }
}
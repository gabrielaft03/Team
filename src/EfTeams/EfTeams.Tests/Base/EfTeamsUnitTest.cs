using EfTeams.Business.Interfaces;
using EfTeams.Business.Services;
using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Repositories.Interfaces;
using EfTeams.Repositories.Repositories;
using EfTeams.Tests.Builder;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTeams.Tests.Base
{
    public abstract class EfTeamsUnitTest
    {
        public TeamDbContext db;
        public TeamDbContext dbSQL;

        //ITeamsService TeamService { get; set; }
        //IPlayerRepository PlayerRepository { get; set; }
        //Mock<IPlayerRepository> PlayerRepositoryMock { get; set; }
        public CoachBuilder coachBuilder { get; set; }
        public CountryBuilder countryBuilder { get; set; }
        public LeagueBuilder leagueBuilder { get; set; }
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
            //PlayerRepository = new PlayerRepository(db);
            if (db.Database.IsInMemory())
            {
                db.Database.EnsureDeleted();
                coachBuilder.AddCoaches(5);
                countryBuilder.AddCountries(5);
                leagueBuilder.AddLeagues(5);
                coachBuilder.Build();
            }
        }
        
    }
}
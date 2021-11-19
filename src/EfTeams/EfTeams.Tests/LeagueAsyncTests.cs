using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Tests.Base;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Tests
{
    public class LeagueAsyncTests: EfTeamsUnitTest
    {
        [Test]
        public async Task AddLeague()
        {
            var league = new League
            { Id = 0, LeagueName = "The League" };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.LeagueRepository.Add(league);
            var result = await unitOfWork .Complete();
            //var processor = new TeamsService(unitOfWork, db);
            //var result = processor.GetPlayerByTeamAsync(league.Id);
            Assert.Greater(league.Id, 0);
        }

        [Test]
        public async Task AddLeagues()
        {
            var Leagues = new List<League>()
            { new League() { Id = 0, LeagueName = "League1" },
              new League() { Id = 0, LeagueName = "League2" }
            };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.LeagueRepository.AddRange(Leagues);
            var result = await unitOfWork .Complete();
            Assert.Greater(Leagues[0].Id, 0);
            Assert.Greater(Leagues[1].Id, 0);
        }

        [Test]
        public async Task GetLeague()
        {
            var league = new League { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.LeagueRepository.Get(league.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.LeagueName);
            Assert.IsNotEmpty(result.LeagueName);
        }

        [Test]
        public async Task GetLeagues()
        {
            var league = new League { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.LeagueRepository.Get(league.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.LeagueName);
            Assert.IsNotEmpty(result.LeagueName);
        }
        [Test]
        public async Task EditLeague()
        {
            var league = new League
            { Id = 1, LeagueName = "League4" };

            using var unitOfWork = new UnitOfWork(db);

            var leagueFromDB = await unitOfWork.LeagueRepository.Get(1);
            leagueFromDB.LeagueName = league.LeagueName;

            await unitOfWork.LeagueRepository.Update(leagueFromDB);
            var completed = await unitOfWork.Complete();
            //unitOfWork.LeagueRepository.context.Leagues.Items[0].LeagueName

            Assert.AreEqual(leagueFromDB.LeagueName, league.LeagueName);
            Assert.IsTrue(completed);
        }

        [Test]
        public async Task EditLeagues()
        {
            var Leagues = new List<League>()
            { new League() { Id = 1, LeagueName = "League1" },
              new League() { Id = 2, LeagueName = "League2" }
            };
            using var unitOfWork = new UnitOfWork(db);

            var LeaguesFromDB = await unitOfWork.LeagueRepository.GetAll();
            LeaguesFromDB.First(c => c.Id == 1).LeagueName = Leagues[0].LeagueName;
            LeaguesFromDB.First(c => c.Id == 2).LeagueName = Leagues[1].LeagueName;

            await unitOfWork.LeagueRepository.UpdateRange(LeaguesFromDB);
            var completed = await unitOfWork .Complete();

            Assert.AreEqual(LeaguesFromDB.First(c => c.Id == 1).LeagueName, Leagues[0].LeagueName);
            Assert.AreEqual(LeaguesFromDB.First(c => c.Id == 2).LeagueName, Leagues[1].LeagueName);
        }

        [Test]
        public async Task DeleteLeague()
        {
            using var unitOfWork = new UnitOfWork(db);
            var LeaguesFromDB = await unitOfWork.LeagueRepository.Get(5);

            await unitOfWork.LeagueRepository.Delete(LeaguesFromDB);
            var completed = await unitOfWork.Complete();

            Assert.IsTrue(completed);
        }

        [Test]
        public async Task DeleteLeagues()
        {
            using var unitOfWork = new UnitOfWork(db);
            var LeaguesFromDB = await unitOfWork.LeagueRepository.GetAll();

            await unitOfWork.LeagueRepository.DeleteRange(LeaguesFromDB);
            var completed = await unitOfWork.Complete();

            Assert.IsTrue(completed);
        }
    }
}

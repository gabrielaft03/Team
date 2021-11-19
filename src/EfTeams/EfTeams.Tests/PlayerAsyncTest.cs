using EfTeams.Business.Services;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Tests.Base;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Tests
{
    public class PlayerAsyncTest : EfTeamsUnitTest
    {
        [Test]
        public async Task AddPlayer()
        {
            var player = new Player
            { Id = 0, PlayerName = "The Player" };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.PlayerRepository.Add(player);
            var result = await unitOfWork .Complete();

            Assert.Greater(player.Id, 0);
        }

        [Test]
        public async Task AddPlayers()
        {
            var players = new List<Player>()
            { new Player() { Id = 0, PlayerName = "Player1" },
              new Player() { Id = 0, PlayerName = "Player2" }
            };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.PlayerRepository.AddRange(players);
            var result = await unitOfWork.Complete();
            Assert.Greater(players[0].Id, 0);
            Assert.Greater(players[1].Id, 0);
        }

        [Test]
        public async Task GetPlayer()
        {
            var player = new Player { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.PlayerRepository.Get(player.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.PlayerName);
            Assert.IsNotEmpty(result.PlayerName);
        }

        [Test]
        public async Task GetPlayers()
        {
            var player = new Player { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.PlayerRepository.Get(player.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.PlayerName);
            Assert.IsNotEmpty(result.PlayerName);
        }
        [Test]
        public async Task EditPlayer()
        {
            var player = new Player
            { Id = 1, PlayerName = "Player4" };

            using var unitOfWork = new UnitOfWork(db);

            var playerFromDB = await unitOfWork.PlayerRepository.Get(1);
            playerFromDB.PlayerName = player.PlayerName;

            await unitOfWork.PlayerRepository.Update(playerFromDB);
            var completed = await unitOfWork.Complete();
            //unitOfWork.PlayerRepository.context.Players.Items[0].PlayerName

            Assert.AreEqual(playerFromDB.PlayerName, player.PlayerName);
            Assert.IsTrue(completed);
        }

        [Test]
        public async Task EditPlayers()
        {
            var players = new List<Player>()
            { new Player() { Id = 1, PlayerName = "Player1" }
            };
            using var unitOfWork = new UnitOfWork(db);

            var playersFromDB = await unitOfWork.PlayerRepository.GetAll();
            playersFromDB.First(c => c.Id == 1).PlayerName = players[0].PlayerName;

            await unitOfWork.PlayerRepository.UpdateRange(playersFromDB);
            var completed = await unitOfWork.Complete();

            Assert.AreEqual(playersFromDB.First(c => c.Id == 1).PlayerName, players[0].PlayerName);
        }

        [Test]
        public async Task DeletePlayer()
        {
            using var unitOfWork = new UnitOfWork(db);
            var playersFromDB = await unitOfWork.PlayerRepository.Get(1);

            await unitOfWork.PlayerRepository.Delete(playersFromDB);
            var completed = await unitOfWork.Complete();

            Assert.IsTrue(completed);
        }

        [Test]
        public async Task DeletePlayers()
        {
            using var unitOfWork = new UnitOfWork(db);
            var playersFromDB = await unitOfWork.PlayerRepository.GetAll();

            await unitOfWork.PlayerRepository.DeleteRange(playersFromDB);
            var completed = await unitOfWork.Complete();

            Assert.IsTrue(completed);
        }

        [Test]
        public async Task GetPlayerAsync()
        {
            var unitOfWork = new UnitOfWork(db);
            var service1 = new TeamsService(unitOfWork, db);

            var player = await service1.GetPlayerByTeamAsync(1);
            Assert.Greater(player.Count(), 0);
        }

        [Test]
        public async Task AddPlayerWithTeamAsync()
        {
            var unitOfWork = new UnitOfWork(db);
            var team = await unitOfWork.TeamRepository.Get(1);

            var service1 = new TeamsService(unitOfWork, db);
            var player = new Player { Id = 0, PlayerName = "Player10", Position = "2", Team = team };
            //var playersFromDB = await unitOfWork.PlayerRepository.GetPlayerAsync(1);
            var result = await service1.AddPlayerWithTeamId(player);

            Assert.IsTrue(result);
        }

    }
}

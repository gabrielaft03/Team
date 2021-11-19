using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Tests.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Tests
{
    public class TeamAsyncTests: EfTeamsUnitTest
    {
        [Test]
        public async Task AddTeam()
        {
            var team = new Team
            { Id = 0, TeamName = "The Team" };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.TeamRepository.Add(team);
            var result = unitOfWork.Complete();
            //var processor = new TeamsService(unitOfWork, db);
            //var result = processor.GetPlayerByTeamAsync(team.Id);
            Assert.Greater(team.Id, 0);
        }

        [Test]
        public async Task AddTeams()
        {
            var teams = new List<Team>()
            { new Team() { Id = 0, TeamName = "Team1" },
              new Team() { Id = 0, TeamName = "Team2" }
            };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.TeamRepository.AddRange(teams);
            var result = unitOfWork.Complete();
            Assert.Greater(teams[0].Id, 0);
            Assert.Greater(teams[1].Id, 0);
        }

        [Test]
        public async Task GetTeam()
        {
            var team = new Team { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.TeamRepository.Get(team.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.TeamName);
            Assert.IsNotEmpty(result.TeamName);
        }

        [Test]
        public async Task GetTeams()
        {
            var team = new Team { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.TeamRepository.Get(team.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.TeamName);
            Assert.IsNotEmpty(result.TeamName);
        }
        [Test]
        public async Task EditTeam()
        {
            var team = new Team
            { Id = 1, TeamName = "Team4" };

            using var unitOfWork = new UnitOfWork(db);

            var teamFromDB = await unitOfWork.TeamRepository.Get(1);
            teamFromDB.TeamName = team.TeamName;

            await unitOfWork.TeamRepository.Update(teamFromDB);
            var completed = unitOfWork.Complete();
            //unitOfWork.TeamRepository.context.Teams.Items[0].TeamName

            Assert.AreEqual(teamFromDB.TeamName, team.TeamName);
            Assert.IsTrue(completed.Result);
        }

        [Test]
        public async Task EditTeams()
        {
            var teams = new List<Team>()
            { new Team() { Id = 1, TeamName = "Team1" }
            };
            using var unitOfWork = new UnitOfWork(db);

            var teamsFromDB = await unitOfWork.TeamRepository.GetAll();
            teamsFromDB.First(c => c.Id == 1).TeamName = teams[0].TeamName;

            await unitOfWork.TeamRepository.UpdateRange(teamsFromDB);
            var completed = unitOfWork.Complete();

            Assert.AreEqual(teamsFromDB.First(c => c.Id == 1).TeamName, teams[0].TeamName);
        }

        [Test]
        public async Task DeleteTeam()
        {
            using var unitOfWork = new UnitOfWork(db);
            var teamsFromDB = await unitOfWork.TeamRepository.Get(1);
            System.Action action = () =>
            {
                unitOfWork.TeamRepository.Delete(teamsFromDB).GetAwaiter().GetResult();
                var completed = unitOfWork.Complete().GetAwaiter().GetResult();
            };
            Assert.Throws<InvalidOperationException>(()=>action());
        }

        [Test]
        public async Task DeleteTeams()
        {
            using var unitOfWork = new UnitOfWork(db);
            var teamsFromDB = await unitOfWork.TeamRepository.GetAll();
            System.Action action = () =>
            {
                unitOfWork.TeamRepository.DeleteRange(teamsFromDB).GetAwaiter().GetResult();
                var completed = unitOfWork.Complete().GetAwaiter().GetResult();
            };
            Assert.Throws<InvalidOperationException>(() => action());
        }
    }
}

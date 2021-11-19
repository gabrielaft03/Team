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
    public class CoachAsyncTests : EfTeamsUnitTest
    {
        //[SetUp]
        //public void Setup()
        //{
        //    base.Setup();
        //}

        [Test]
        public async Task AddCoach()
        {
            var coach = new Coach
            { Id = 0, CoachName = "Natalie Malague" };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.CoachRepository.Add(coach);
            var result = await unitOfWork .Complete();
            //var processor = new TeamsService(unitOfWork, db);
            //var result = processor.GetPlayerByTeamAsync(coach.Id);
            Assert.Greater(coach.Id, 0);
        }

        [Test]
        public async Task AddCoaches()
        {
            var coaches = new List<Coach>()
            { new Coach() { Id = 0, CoachName = "Dendi" },
              new Coach() { Id = 0, CoachName = "ArtStyle" }
            };

            var unitOfWork = new UnitOfWork(db);

            await unitOfWork.CoachRepository.AddRange(coaches);
            var result =  await unitOfWork .Complete();
            Assert.Greater(coaches[0].Id, 0);
            Assert.Greater(coaches[1].Id, 0);
            Assert.True(result);
        }

        [Test]
        public async Task GetCoach()
        {
            var coach = new Coach { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.CoachRepository.Get(coach.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.CoachName);
            Assert.IsNotEmpty(result.CoachName);
        }

        [Test]
        public async Task GetCoaches()
        {
            var coach = new Coach { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = await unitOfWork.CoachRepository.Get(coach.Id);

            Assert.Greater(result.Id, 0);
            Assert.IsNotNull(result.CoachName);
            Assert.IsNotEmpty(result.CoachName);
        }
        [Test]
        public async Task EditCoach()
        {
            var coach = new Coach
            { Id = 1, CoachName = "Dendimon" };

            using var unitOfWork = new UnitOfWork(db);

            var coachFromDB = await unitOfWork.CoachRepository.Get(1);
            coachFromDB.CoachName = coach.CoachName;

            await unitOfWork.CoachRepository.Update(coachFromDB);
            var completed = await unitOfWork.Complete();
            //unitOfWork.CoachRepository.context.Coaches.Items[0].CoachName

            Assert.AreEqual(coachFromDB.CoachName, coach.CoachName);
            Assert.IsTrue(completed);
        }

        [Test]
        public async Task EditCoaches()
        {
            var coaches = new List<Coach>()
            { new Coach() { Id = 1, CoachName = "Dendi" },
              new Coach() { Id = 2, CoachName = "ArtStyle" }
            };
            using var unitOfWork = new UnitOfWork(db);

            var coachesFromDB = await unitOfWork.CoachRepository.GetAll();
            coachesFromDB.First(c => c.Id == 1).CoachName= coaches[0].CoachName;
            coachesFromDB.First(c => c.Id == 2).CoachName= coaches[1].CoachName;

            await unitOfWork.CoachRepository.UpdateRange(coachesFromDB);
            var completed = await unitOfWork.Complete();

            Assert.AreEqual(coachesFromDB.First(c => c.Id == 1).CoachName, coaches[0].CoachName);
            Assert.AreEqual(coachesFromDB.First(c => c.Id == 2).CoachName, coaches[1].CoachName);
        }

        [Test]
        public async Task DeleteCoach()
        {
            using var unitOfWork = new UnitOfWork(db);
            var coachesFromDB = await unitOfWork.CoachRepository.Get(5);

            await unitOfWork.CoachRepository.Delete(coachesFromDB);
            var completed = await unitOfWork.Complete();

            Assert.IsTrue(completed);
        }

        [Test]
        public async Task DeleteCoaches()
        {

            using var unitOfWork = new UnitOfWork(db);
            var coachesFromDB = await unitOfWork.CoachRepository.GetAll();
            System.Action action = () =>
            {
                unitOfWork.CoachRepository.DeleteRange(coachesFromDB).GetAwaiter().GetResult();
                var completed = unitOfWork.Complete().GetAwaiter().GetResult();
            };
            Assert.Throws<InvalidOperationException>(() => action());
        }



    }
}

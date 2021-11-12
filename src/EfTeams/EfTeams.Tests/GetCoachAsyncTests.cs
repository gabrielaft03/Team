using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Tests.Base;
using NUnit.Framework;
using System.Collections.Generic;

namespace EfTeams.Tests
{
    public class GetCoachAsyncTests : EfTeamsUnitTest
    {
        //[SetUp]
        //public void Setup()
        //{
        //    base.Setup();
        //}

        [Test]
        public void AddCoach()
        {
            var coach = new Coach
            { Id = 0, CoachName = "Natalie Malague" };
            //var db = GetMemoryContext();
            var unitOfWork = new UnitOfWork(dbSQL);

            unitOfWork.CoachRepository.Add(coach);
            var result = unitOfWork.Complete();
            //var processor = new TeamsService(unitOfWork, db);
            //var result = processor.GetPlayerByTeamAsync(coach.Id);
            Assert.Greater(coach.Id, 0);
        }

        [Test]
        public void AddCoaches()
        {
            var coaches = new List<Coach>()
            { new Coach() { Id = 0, CoachName = "Dendi" },
              new Coach() { Id = 0, CoachName = "ArtStyle" }
            };
            //var db = GetMemoryContext();
            var unitOfWork = new UnitOfWork(db);

            unitOfWork.CoachRepository.AddRange(coaches);
            var result = unitOfWork.Complete();
            Assert.Greater(coaches[0].Id, 0);
            Assert.Greater(coaches[1].Id, 0);
        }

        [Test]
        public void GetCoach()
        {
            var coach = new Coach { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = unitOfWork.CoachRepository.Get(coach.Id);
            Assert.Greater(result.Result.Id, 0);
            Assert.IsNotNull(result.Result.CoachName);
            Assert.IsNotEmpty(result.Result.CoachName);
        }

        [Test]
        public void GetCoaches()
        {
            var coach = new Coach { Id = 1 };
            var unitOfWork = new UnitOfWork(db);

            var result = unitOfWork.CoachRepository.Get(coach.Id);

            Assert.Greater(result.Result.Id, 0);
            Assert.IsNotNull(result.Result.CoachName);
            Assert.IsNotEmpty(result.Result.CoachName);
        }
        [Test]
        public void EditCoach()
        {
            var coach = new Coach
            { Id = 1, CoachName = "Dendimon" };
            var unitOfWork = new UnitOfWork(db);
            var coachResult = unitOfWork.CoachRepository.Update(coach);
            var completed = unitOfWork.Complete();
            //unitOfWork.CoachRepository.context.Coaches.Items[0].CoachName

            Assert.AreEqual(coach.CoachName, "Dendimon");
            Assert.IsTrue(completed.Result);
        }

        [Test]
        public void EditCoaches()
        {
            var coaches = new List<Coach>()
            { new Coach() { Id = 1, CoachName = "Dendi" },
              new Coach() { Id = 2, CoachName = "ArtStyle" }
            };
            var unitOfWork = new UnitOfWork(db);

            unitOfWork.CoachRepository.UpdateRange(coaches);
            var result = unitOfWork.Complete();

            Assert.Greater(coaches[0].Id, 0);
            Assert.Greater(coaches[1].Id, 0);
            Assert.AreEqual(coaches[1].CoachName, "Dendi");
            Assert.AreEqual(coaches[1].CoachName, "ArtStyle");
        }

        [Test]
        public void DeleteCoach()
        {
            var coach = new Coach
            { Id = 1, CoachName = "Dendimon" };
            var unitOfWork = new UnitOfWork(db);
            var coachResult = unitOfWork.CoachRepository.Delete(coach);
            var completed = unitOfWork.Complete();

            Assert.IsNull(coach);
            Assert.IsTrue(completed.Result);
        }

        [Test]
        public void DeleteCoaches()
        {
            var coaches = new List<Coach>()
            { new Coach() { Id = 1012}//,
              //new Coach() { Id = 1010}
            };
            var unitOfWork = new UnitOfWork(dbSQL);

            unitOfWork.CoachRepository.DeleteRange(coaches);
            var result = unitOfWork.Complete();

            Assert.IsTrue(result.Result);
        }

    }
}

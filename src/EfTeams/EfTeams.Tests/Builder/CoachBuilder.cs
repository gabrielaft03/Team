using Bogus;
using EfTeams.Data;
using EfTeams.Data.Models;

namespace EfTeams.Tests.Builder
{
    public class CoachBuilder
    {
        private readonly TeamDbContext _dbContext;

        public CoachBuilder(TeamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Build()
        {
            _dbContext.SaveChanges();
        }
        
        public void AddCoaches(int count = 1)
        {
            var coachFaker = new Faker<Coach>().RuleFor(x=>x.CoachName, f=>f.Name.FirstName());
            var coaches = coachFaker.Generate(count);
            _dbContext.AddRange(coaches);
        }

    }
}

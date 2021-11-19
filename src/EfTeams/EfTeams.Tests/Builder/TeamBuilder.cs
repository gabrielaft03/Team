using Bogus;
using EfTeams.Data;
using EfTeams.Data.Models;
using System.Linq;

namespace EfTeams.Tests.Builder
{
    public class TeamBuilder
    {
        private TeamDbContext _dbContext;

        public TeamBuilder(TeamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Build()
        {
            _dbContext.SaveChanges();
        }

        public void AddTeams(int count)
        {
            var teamFaker = new Faker<Team>().RuleFor(x => x.TeamName, f => f.Name.LastName())
                .RuleFor(x => x.Abbreviation, f => f.Internet.UserName())
                .RuleFor(x => x.Coach, c=> c.PickRandom<Coach>(_dbContext.Coaches.FirstOrDefault()))
                .RuleFor(x=>x.Country, c=>c.PickRandom<Country>(_dbContext.Countries.FirstOrDefault()));
                //.RuleFor(x=>x.Team, t=>t.PickRandom(coachbuilder1);
                //.RuleFor(x=>x.Team.);
                //.With(o => o.Customer = Pick<Customer>.RandomItemFrom(customers))

            var teams = teamFaker.Generate(count);
            _dbContext.AddRange(teams);

            //var coachFaker = new Faker<Coach>().RuleFor(x => x.CoachName, f => f.Name.FirstName());
            //var coaches = coachFaker.Generate(count);
            //_dbContext.AddRange(coaches);
        }

    }
}

using Bogus;
using EfTeams.Data;
using EfTeams.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTeams.Tests.Builder
{
    public class LeagueBuilder
    {
        private readonly TeamDbContext _dbContext;

        public LeagueBuilder(TeamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Build()
        {
            _dbContext.SaveChanges();
        }
        public void AddLeagues(int count = 1)
        {
            var leagueFaker = new Faker<League>().RuleFor(x => x.LeagueName, f => f.Company.CompanyName());
            var leagues = leagueFaker.Generate(count);
            _dbContext.AddRange(leagues);
        }
    }
}

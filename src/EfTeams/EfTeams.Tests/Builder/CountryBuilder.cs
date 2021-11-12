using Bogus;
using EfTeams.Data;
using EfTeams.Data.Models;

namespace EfTeams.Tests.Builder
{
    public class CountryBuilder
    {
        private readonly TeamDbContext _dbContext;

        public CountryBuilder(TeamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Build()
        {
            _dbContext.SaveChanges();
        }
        public void AddCountries(int count = 1)
        {
            var countryFaker = new Faker<Country>().RuleFor(x => x.CountryName, f => f.Address.Country());
            var countries = countryFaker.Generate(count);
            _dbContext.AddRange(countries);
        }
    }
}

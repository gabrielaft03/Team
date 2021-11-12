using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Interfaces;
using EfTeams.Repositories.Repositories;
using System;
using System.Threading.Tasks;

namespace EfTeams.Repositories.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositories
        ITeamLeagueRepository TeamLeagueRepository { get; }
        IRepository<Coach> CoachRepository { get; }
        IRepository<League> LeagueRepository { get; }
        IRepository<Country> CountryRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        ITeamRepository TeamRepository { get; }
        Task<bool> Complete();
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeamDbContext context;
        //private readonly IRepository<Country> _countryRepository;
        public UnitOfWork(TeamDbContext context /*, IRepository<Country> countryRepository*/)
        {
            this.context = context;
           // _countryRepository = countryRepository;
            //CountryRepo = _countryRepository;
        }

        #region Repositories

        //Repositories

        private readonly IRepository<Country> countryRepository;
        //public IRepository<Country> CountryRepo { get; }
        public IRepository<Country> CountryRepository => countryRepository ?? new Repository<Country>(context);

        private readonly IRepository<Coach> coachRepository;
        public IRepository<Coach> CoachRepository => coachRepository ?? new Repository<Coach>(context);

        private readonly IRepository<League> leagueRepository;
        public IRepository<League> LeagueRepository => leagueRepository ?? new Repository<League>(context);

        private readonly IPlayerRepository playerRepository;
        public IPlayerRepository PlayerRepository => playerRepository ?? new PlayerRepository(context);

        private readonly ITeamRepository teamRepository;
        public ITeamRepository TeamRepository => teamRepository ?? new TeamRepository(context);

        private readonly ITeamLeagueRepository teamLeagueRepository;
        public ITeamLeagueRepository TeamLeagueRepository => teamLeagueRepository ?? new TeamLeagueRepository(context);

        #endregion

        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;
        public async Task<int> SaveChangesAsync()
    => await context.SaveChangesAsync();

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

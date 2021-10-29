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
        public UnitOfWork(TeamDbContext context)
        {
            this.context = context;
        }

        #region Repositories

        //Repositories

        private IRepository<Country> countryRepository;
        public IRepository<Country> CountryRepository => countryRepository ?? new Repository<Country>(context);

        private IRepository<Coach> coachRepository;
        public IRepository<Coach> CoachRepository => coachRepository ?? new Repository<Coach>(context);

        private IRepository<League> leagueRepository;
        public IRepository<League> LeagueRepository => leagueRepository ?? new Repository<League>(context);

        private IPlayerRepository playerRepository;
        public IPlayerRepository PlayerRepository => playerRepository ?? new PlayerRepository(context);

        private ITeamRepository teamRepository;
        public ITeamRepository TeamRepository => teamRepository ?? new TeamRepository(context);

        private ITeamLeagueRepository teamLeagueRepository;
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

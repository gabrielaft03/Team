using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Services.Interfaces;
using EfTeams.Services.Repositories;
using System;
using System.Threading.Tasks;

namespace EfTeams.Services.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositories
        //ILeagueRepository LeagueRepository { get; }
        IRepository<Country> CountryRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        Task<bool> Complete();
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
        //private ILeagueRepository leagueRepository;
        //public ILeagueRepository LeagueRepository => leagueRepository ?? new LeagueRepository(context);
        private IRepository<Country> countryRepository;
        public IRepository<Country> CountryRepository =>
            countryRepository ?? new Repository<Country>(context);

        private IPlayerRepository playerRepository;
        public IPlayerRepository PlayerRepository =>
            playerRepository ?? new PlayerRepository(context);

        #endregion

        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

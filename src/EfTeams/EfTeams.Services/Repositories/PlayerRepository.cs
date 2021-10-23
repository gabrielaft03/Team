using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Services.Generic;
using EfTeams.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Services.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(TeamDbContext context) : base(context)
        {

        }

        //aqui solo add player con teamid
        public async Task<IEnumerable<Player>> GetPlayerAsync(int teamId)
            => await context.Players.Where(n => n.TeamId == teamId)
                  .OrderByDescending(n => n.PlayerName)
                  .ToListAsync();

    }
}

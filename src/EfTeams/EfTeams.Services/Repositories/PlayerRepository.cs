using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Repositories.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(TeamDbContext context) : base(context)
        {

        }

        public override Task Delete(Player entity)
        {
            //entity.Team.
            return base.Delete(entity);
        }

        public async Task<IEnumerable<Player>> GetPlayerAsync(int teamId)
            => await context.Players.Where(n => n.TeamId == teamId)
                  .OrderByDescending(n => n.PlayerName)
                  .ToListAsync();
    }
}

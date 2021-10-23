using EfTeams.Data.Models;
using EfTeams.Services.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Services.Interfaces
{
    public interface IPlayerRepository: IRepository<Player>
    {
        Task<IEnumerable<Player>> GetPlayerAsync(int teamId);
       // Task<Player> AddPlayerWithTeamId(Player player);
    }
}

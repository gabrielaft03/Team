using EfTeams.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Business.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId);
        Task<Player> AddPlayerWithTeamId(Player player);
    }
}

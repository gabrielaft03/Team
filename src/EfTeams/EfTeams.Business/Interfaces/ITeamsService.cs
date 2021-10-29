using EfTeams.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Business.Interfaces
{
    public interface ITeamsService
    {
        Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId);
        Task<bool> AddPlayerWithTeamId(Player player);
        Task<bool> AddPlayerWithTeamIdV3(Player player);
    }
}

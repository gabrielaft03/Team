using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Repositories.Interfaces
{
    public interface IPlayerRepository: IRepository<Player>
    {
        Task<IEnumerable<Player>> GetPlayerAsync(int teamId);
    }
}

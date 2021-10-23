using EfTeams.Business.Interfaces;
using EfTeams.Data.Models;
using EfTeams.Services.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTeams.Business.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork unitOfWork;
        // private readonly IHttpContextReader httpContextReader;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId)
            => await unitOfWork.PlayerRepository.GetPlayerAsync(teamId);

        public async Task<Player> AddPlayerWithTeamId(Player player)
        {
            await unitOfWork.PlayerRepository.Add(player);
            //crear team o agarrar  el q existe
            return await unitOfWork.Complete() ? player : null;
        }

    }   
}

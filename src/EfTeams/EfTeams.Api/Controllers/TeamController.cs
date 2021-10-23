using EfTeams.Data.Models;
using EfTeams.Services.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        // private readonly ITeamsService<TeamsService> _teamService;
        private readonly IRepository<Team> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(ILogger<TeamController> logger, /*, ITeamsService<TeamsService> teamService*/
                                IRepository<Team> genericRepository,
                                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;
            //_teamService = teamService;
        }

        [Route("~/GetPlayer"), HttpGet]
        public async Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId)
        {
            var result = await _unitOfWork.PlayerRepository.GetPlayerAsync(teamId);
            return result;
        }

        [Route("~/AddPlayer"), HttpPost]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPlayerAsync(Player player)
        {
            await _unitOfWork.PlayerRepository.Add(player);
            return await _unitOfWork.Complete() ? Ok(player) : BadRequest();
        }

        [Route("~/AddPlayerAsync"), HttpPost]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPlayerJsonAsync(Player player)
        {
            await _unitOfWork.PlayerRepository.Add(player);
            var success = await _unitOfWork.Complete();
            var playerJson = JsonConvert.SerializeObject(player, new JsonSerializerSettings 
                            { 
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                            });
            return success ? Ok(playerJson) : BadRequest();
        }

    }
}

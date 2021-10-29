using EfTeams.Business.Interfaces;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Api.Controllers
{

    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamsService _teamService;
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(ILogger<TeamController> logger, ITeamsService teamService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this._unitOfWork = unitOfWork;
            _teamService = teamService;
        }

        #region League
        [Route("~/league"), HttpGet]
        public async Task<League> GetLeagueAsync(int id)
        {
            var result = await _unitOfWork.LeagueRepository.Get(id);
            return result;
        }

        [Route("~/league"), HttpPost]
        public async Task<int> AddLeagueAsync(League league)
        {
            await _unitOfWork.LeagueRepository.Add(league);
            await _unitOfWork.Complete();
            return league.Id;
        }

        [Route("~/league"), HttpPut]
        public async Task<League> EditTeamAsync(League league)
        {
            await _unitOfWork.LeagueRepository.Update(league);
            await _unitOfWork.Complete();
            return league;
        }

        [Route("~/league"), HttpDelete]
        public async Task<bool> LeagueCoachAsync(League league)
        {
            await _unitOfWork.LeagueRepository.Delete(league);
            return await _unitOfWork.Complete();
        }
        #endregion League

        #region Team
        [Route("~/team"), HttpGet]
        public async Task<Team> GetTeamAsync(int id)
        {
            var result = await _unitOfWork.TeamRepository.Get(id);
            return result;
        }

        [Route("~/teams"), HttpGet]
        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var result = await _unitOfWork.TeamRepository.GetAll();
            return result;
        }

        [Route("~/team"), HttpPost]
        public async Task<int> AddTeamAsync(Team team)
        {
            await _unitOfWork.TeamRepository.Add(team);
            await _unitOfWork.Complete();
            return team.Id;
        }

        [Route("~/teams"), HttpPost]
        public async Task<IEnumerable<Team>> AddTeamsAsync(IEnumerable<Team> team)
        {
            await _unitOfWork.TeamRepository.AddRange(team);
            await _unitOfWork.Complete();
            return team;
        }

        [Route("~/team"), HttpPut]
        public async Task<Team> EditTeamAsync(Team team)
        {
            await _unitOfWork.TeamRepository.Update(team);
            await _unitOfWork.Complete();
            return team;
        }

        [Route("~/teams"), HttpPut]
        public async Task<IEnumerable<Team>> EditTeamsAsync(IEnumerable<Team> team)
        {
            await _unitOfWork.TeamRepository.UpdateRange(team);
            await _unitOfWork.Complete();
            return team;
        }

        [Route("~/team"), HttpDelete]
        public async Task<bool> DeleteTeamAsync(Team team)
        {
            await _unitOfWork.TeamRepository.Delete(team);
            return await _unitOfWork.Complete();
        }

        [Route("~/teams"), HttpDelete]
        public async Task<IEnumerable<Team>> DeleteTeamsAsync(IEnumerable<Team> team)
        {
            await _unitOfWork.TeamRepository.DeleteRange(team);
            await _unitOfWork.Complete();
            return team;
        }
        #endregion Team

        #region Coach
        [Route("~/coach"), HttpGet]
        public async Task<Coach> GetCoachAsync(int id)
        {
            var result = await _unitOfWork.CoachRepository.Get(id);
            return result;
        }

        [Route("~/coaches"), HttpGet]
        public async Task<IEnumerable<Coach>> GetAllCoachAsync()
        {
            var result = await _unitOfWork.CoachRepository.GetAll();
            return result;
        }

        [Route("~/coach/US"), HttpGet]
        public async Task<Coach> GetCoachUSAsync()
        {
            //Find the only Coach in US and return it. Just an example to use Find.
            var result = await _unitOfWork.CoachRepository.Find(n => n.Id == 1);
            return result;
        }

        [Route("~/coach"), HttpPost]
        public async Task<int> AddCoachAsync(Coach coach)
        {
            await _unitOfWork.CoachRepository.Add(coach);
            await _unitOfWork.Complete();
            return coach.Id;
        }

        [Route("~/coaches"), HttpPost]
        public async Task<IEnumerable<Coach>> AddCoachesAsync(IEnumerable<Coach> coach)
        {
            await _unitOfWork.CoachRepository.AddRange(coach);
            await _unitOfWork.Complete();
            return coach;
        }

        [Route("~/coach"), HttpPut]
        public async Task<Coach> EditCoachAsync(Coach coach)
        {
            await _unitOfWork.CoachRepository.Update(coach);
            await _unitOfWork.Complete();
            return coach;
        }

        [Route("~/coaches"), HttpPut]
        public async Task<IEnumerable<Coach>> EditCoachesAsync(IEnumerable<Coach> coach)
        {
            await _unitOfWork.CoachRepository.UpdateRange(coach);
            await _unitOfWork.Complete();
            return coach;
        }

        [Route("~/coach"), HttpDelete]
        public async Task<bool> DeleteCoachAsync(Coach coach)
        {
            await _unitOfWork.CoachRepository.Delete(coach);
            return await _unitOfWork.Complete();
        }

        [Route("~/coaches"), HttpDelete]
        public async Task<IEnumerable<Coach>> DeleteCoachesAsync(IEnumerable<Coach> coach)
        {
            await _unitOfWork.CoachRepository.DeleteRange(coach);
            await _unitOfWork.Complete();
            return coach;
        }

        #endregion Coach

        #region Player

        [Route("~/player"), HttpDelete]
        public async Task<bool> DeletePlayerAsync(Player player)
        {
            await _unitOfWork.PlayerRepository.Delete(player);
            return await _unitOfWork.Complete();
        }

        [Route("~/playersByTeam"), HttpGet]
        public async Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId)
        {
            //Get with Where
            var result = await _unitOfWork.PlayerRepository.GetPlayerAsync(teamId);
            return result;
        }

        [Route("~/playerAsync"), HttpPost]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPlayerJsonAsync(Player player)
        {
            var success = await _teamService.AddPlayerWithTeamId(player);
            var playerJson = JsonConvert.SerializeObject(player, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return success ? Ok(playerJson) : BadRequest();
        }


        [Route("~/playerTransactionAsync"), HttpPost]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPlayerJsonAsyncV3(Player player)
        {
            var success = await _teamService.AddPlayerWithTeamIdV3(player);
            var playerJson = JsonConvert.SerializeObject(player, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return success ? Ok(playerJson) : BadRequest();
        }

        #endregion Player

    }
}

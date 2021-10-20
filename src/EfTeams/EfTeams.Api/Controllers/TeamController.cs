using EfTeams.Data.Models;
using EfTeams.Services.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfTeams.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        // private readonly ITeamsService<TeamsService> _teamService;
        private readonly IGenericRepository<Team> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(ILogger<TeamController> logger, /*, ITeamsService<TeamsService> teamService*/ 
                                IGenericRepository<Team> genericRepository, 
                                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;
            //_teamService = teamService;
        }

        // GET: api/<TeamController>
        [HttpGet]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            //return await _context.Games.ToListAsync();
            return await _genericRepository.GetAsync(a => a.CountryId > 0, a => a.OrderByDescending(b => b.TeamName), "Coach,Country");

        }
    }
}

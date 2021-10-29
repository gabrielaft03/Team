using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Repositories.Interfaces;

namespace EfTeams.Repositories.Repositories
{
    public class TeamLeagueRepository : Repository<TeamLeague>, ITeamLeagueRepository
    {
        public TeamLeagueRepository(TeamDbContext context) : base(context)
        {

        }


    }
}

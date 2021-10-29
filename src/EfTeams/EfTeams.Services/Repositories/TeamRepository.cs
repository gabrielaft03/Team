using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using EfTeams.Repositories.Interfaces;

namespace EfTeams.Repositories.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(TeamDbContext context) : base(context)
        {
        }

    }
}

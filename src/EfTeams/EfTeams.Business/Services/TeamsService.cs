using EfTeams.Business.Interfaces;
using EfTeams.Data;
using EfTeams.Data.Models;
using EfTeams.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfTeams.Business.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly TeamDbContext context;

        public TeamsService(IUnitOfWork unitOfWork, TeamDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayerByTeamAsync(int teamId)
            => await unitOfWork.PlayerRepository.GetPlayerAsync(teamId);

        public async Task<bool> AddPlayerWithTeamId(Player player)
        {
            //context.Players.Remove(player);
            try
            {
                if (player.TeamId > 0 || player.Team.Id > 0)
                {
                    //Means that the team exists - verify that exists
                    var teamId = player.TeamId > 0 ? player.TeamId : player.Team.Id;
                    var team = unitOfWork.TeamRepository.Get(teamId);
                    if (team.Result == null)
                    {
                        return false;
                    }
                }
                else
                {
                    //Verify if country exists
                    var countryId = player.Team.CountryId > 0 ? player.Team.CountryId : player.Team.Country.Id;
                    var country = unitOfWork.CountryRepository.Get(countryId);
                    if (country.Result == null)
                    {
                        //Country must exists. I'm not adding a new country.
                        return false;
                    }

                    //Takes the one that has the id
                    var coachId = player.Team.CoachId > 0 ? player.Team.CoachId : player.Team.Coach.Id;

                    var coach = unitOfWork.CoachRepository.Get(coachId);
                    if (coach.Result == null)
                    {
                        await unitOfWork.CoachRepository.Add(player.Team.Coach);
                    }

                    await unitOfWork.TeamRepository.Add(player.Team);
                }

                await unitOfWork.PlayerRepository.Add(player);

                return await unitOfWork.Complete();
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> AddPlayerWithTeamIdV3(Player player)
        {
            using var transaction = context.Database.BeginTransactionAsync();
            try
            {
                if (player.TeamId > 0 || player.Team.Id > 0)
                {
                    //Means that the team exists - verify that exists
                    var teamId = player.TeamId > 0 ? player.TeamId : player.Team.Id;
                    var team = unitOfWork.TeamRepository.Get(teamId);
                    if (team.Result == null)
                    {
                        //log error
                        return false;
                    }
                }
                else
                {
                    //Don't verify if country exists, because the transaction will handle it.
                    var coachId = player.Team.CoachId > 0 ? player.Team.CoachId : player.Team.Coach.Id;
                    //CoachId is zero when it doesn't exist
                    var coach = unitOfWork.CoachRepository.Get(coachId);
                    if (coach.Result == null)
                    {
                        await unitOfWork.CoachRepository.Add(player.Team.Coach);
                        await unitOfWork.SaveChangesAsync();
                    }
                    await unitOfWork.TeamRepository.Add(player.Team);
                    await unitOfWork.SaveChangesAsync();
                }

                await unitOfWork.PlayerRepository.Add(player);
                await unitOfWork.SaveChangesAsync();

                transaction.Result.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

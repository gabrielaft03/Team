using EfTeams.Business.Interfaces;
using EfTeams.Business.Services;
using EfTeams.Repositories.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace EfTeams.Api.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITeamsService, TeamsService>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //Inject generic repository
            return services;
        }
    }
}

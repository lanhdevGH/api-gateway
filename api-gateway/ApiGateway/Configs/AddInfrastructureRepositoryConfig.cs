using ApiGateway.Application.Interfaces;
using ApiGateway.Infrastructure.Persistence.Repositories;

namespace ApiGateway.Configs
{
    public static class AddInfrastructureRepositoryConfig
    {
        public static void AddEFRepository(this IServiceCollection services)
        {
            services.AddScoped<IRouteRepositories, RouteRepositories>();
        }
    }
}

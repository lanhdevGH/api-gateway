using ApiGateway.Application.Features.Routes;
using ApiGateway.Application.Provider;
using ApiGateway.Application.Provider.Implement;

namespace ApiGateway.Configs
{
    public static class AddApplicationProviderConfig
    {
        public static void AddRouterProvider(this IServiceCollection services)
        {
            services.AddScoped<IRouteProvider, RouteWithDBProvider>();
        }
    }
}

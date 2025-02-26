using ApiGateway.Application.Features.Routes;

namespace ApiGateway.Configs
{
    public static class AddApplicationFeatureConfig
    {
        public static void AddRouteFeature(this IServiceCollection services)
        {
            services.AddScoped<GetAllRouteInSystem>();
            services.AddScoped<GetRouteByPath>();
            services.AddScoped<CreateNewRoute>();
        }
    }
}

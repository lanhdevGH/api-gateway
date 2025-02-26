using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Application.Provider;
using ApiGateway.Domain;

namespace ApiGateway.Application.Features.Routes
{
    public class GetRouteByPath
    {
        private readonly IRouteProvider _routeProvider;
        public GetRouteByPath(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }
        public async Task<RouteConfigResponseDTO?> ExecuteAsync(string path)
        {
            return await _routeProvider.GetRouteByPathAsync(path);
        }
    }
}

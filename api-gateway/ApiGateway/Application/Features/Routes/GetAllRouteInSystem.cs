using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Application.Provider;
using ApiGateway.Domain;

namespace ApiGateway.Application.Features.Routes
{
    public class GetAllRouteInSystem
    {
        private readonly IRouteProvider _routeProvider;

        public GetAllRouteInSystem(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }

        public async Task<List<RouteConfigResponseDTO>> ExecuteAsync()
        {
            return await _routeProvider.GetAllRoutesAsync();
        }
    }
}

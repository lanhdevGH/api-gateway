using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Application.Provider;

namespace ApiGateway.Application.Features.Routes
{
    public class CreateNewRoute
    {
        private readonly IRouteProvider _routeProvider;

        public CreateNewRoute(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }

        public async Task<List<RouteConfigResponseDTO>> ExecuteAsync(RouteConfigRequestDTO routeConfigRequest)
        {
            var createdRoute = await _routeProvider.CreateRouteAsync(routeConfigRequest);
            return new List<RouteConfigResponseDTO> { createdRoute };
        }
    }
}

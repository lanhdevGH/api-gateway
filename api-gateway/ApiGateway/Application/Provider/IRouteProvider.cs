using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Domain;

namespace ApiGateway.Application.Provider
{
    public interface IRouteProvider
    {
        Task<List<RouteConfigResponseDTO>> GetAllRoutesAsync();
        Task<RouteConfigResponseDTO> CreateRouteAsync(RouteConfigRequestDTO routeConfigModel);
        Task UpdateRouteAsync(RouteConfigRequestDTO routeConfigModel);
        Task DeleteRouteAsync(int id);
        Task<RouteConfigResponseDTO?> GetRouteByPathAsync(string path);
    }
}

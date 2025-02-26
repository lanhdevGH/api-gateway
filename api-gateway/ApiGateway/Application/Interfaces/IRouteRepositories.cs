using ApiGateway.Domain;

namespace ApiGateway.Application.Interfaces
{
    public interface IRouteRepositories
    {
        Task<List<RouteConfigModel>> GetAllAsync();
        Task<RouteConfigModel> AddAsync(RouteConfigModel RouteConfigModel);
        Task UpdateAsync(RouteConfigModel RouteConfigModel);
        Task DeleteAsync(int id);
        Task<RouteConfigModel?> GetByPathAsync(string path);
    }
}

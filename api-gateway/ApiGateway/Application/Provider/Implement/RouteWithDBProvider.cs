using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Application.Interfaces;
using ApiGateway.Domain;
using AutoMapper;

namespace ApiGateway.Application.Provider.Implement
{
    public class RouteWithDBProvider : IRouteProvider
    {
        private readonly IRouteRepositories _routeRepositories;
        private readonly IMapper _mapper;

        public RouteWithDBProvider(IMapper mapper, IRouteRepositories routeRepositories)
        {
            _mapper = mapper;
            _routeRepositories = routeRepositories;
        }
        public async Task<RouteConfigResponseDTO> CreateRouteAsync(RouteConfigRequestDTO routeConfigRequestDTO)
        {
            var routeConfigModel = _mapper.Map<RouteConfigModel>(routeConfigRequestDTO);
            var addedRouteConfigModel = await _routeRepositories.AddAsync(routeConfigModel);
            return _mapper.Map<RouteConfigResponseDTO>(addedRouteConfigModel);
        }

        public async Task DeleteRouteAsync(int id)
        {
            await _routeRepositories.DeleteAsync(id);
        }

        public async Task<List<RouteConfigResponseDTO>> GetAllRoutesAsync()
        {
            var routeConfigModels = await _routeRepositories.GetAllAsync();
            return _mapper.Map<List<RouteConfigResponseDTO>>(routeConfigModels);
        }

        public async Task<RouteConfigResponseDTO?> GetRouteByPathAsync(string path)
        {
            var routeConfigModel = await _routeRepositories.GetByPathAsync(path);
            return _mapper.Map<RouteConfigResponseDTO?>(routeConfigModel);
        }

        public async Task UpdateRouteAsync(RouteConfigRequestDTO routeConfigRequestDTO)
        {
            var routeConfigModel = _mapper.Map<RouteConfigModel>(routeConfigRequestDTO);
            await _routeRepositories.UpdateAsync(routeConfigModel);
        }
    }
}

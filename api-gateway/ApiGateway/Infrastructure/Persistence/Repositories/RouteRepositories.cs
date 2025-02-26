using ApiGateway.Application.Interfaces;
using ApiGateway.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Persistence.Repositories
{
    public class RouteRepositories : IRouteRepositories
    {
        private readonly ApiGatewayDBContext _context;

        public RouteRepositories(ApiGatewayDBContext context)
        {
            _context = context;
        }

        public async Task<RouteConfigModel> AddAsync(RouteConfigModel routeConfigModel)
        {
            var existingRoute = await _context.Routes
                .FirstOrDefaultAsync(r => r.Path == routeConfigModel.Path);
            
            if (existingRoute != null)
            {
                throw new InvalidOperationException($"Route với path '{routeConfigModel.Path}' đã tồn tại.");
            }

            await _context.Routes.AddAsync(routeConfigModel);
            await _context.SaveChangesAsync();
            return routeConfigModel;
        }

        public async Task DeleteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route != null)
            {
                _context.Routes.Remove(route);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<RouteConfigModel>> GetAllAsync()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task<RouteConfigModel?> GetByPathAsync(string path)
        {
            return await _context.Routes
                .FirstOrDefaultAsync(r => r.Path == path);
        }

        public async Task UpdateAsync(RouteConfigModel routeConfigModel)
        {
            _context.Routes.Update(routeConfigModel);
            await _context.SaveChangesAsync();
        }
    }
}

using ApiGateway.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Configs
{
    public static class AddEFDBConfig
    {
        public static void AddEFDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiGatewayDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}

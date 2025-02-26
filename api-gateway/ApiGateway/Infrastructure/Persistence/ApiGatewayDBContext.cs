using ApiGateway.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Persistence
{
    public class ApiGatewayDBContext : DbContext
    {
        public ApiGatewayDBContext(DbContextOptions<ApiGatewayDBContext> options) : base(options) { }

        public DbSet<RouteConfigModel> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteConfigModel>().HasKey(r => r.Path); // Path làm khóa chính
        }
    }
}

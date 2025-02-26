using ApiGateway.Controllers.Actions;

namespace ApiGateway.Controllers.Endpoint.v1
{
    public static class ApiGatewayEndpoints
    {
        public static void MapGatewayEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/gateway")
                          .WithTags("Gateway");

            // GET: Lấy danh sách ApiGateWay có phân trang
            group.MapGet("/", ApiGatewayActions.GetAllRouteInSystem)
                .WithName("GetApiGateWays")
                .WithOpenApi();

            // POST: Tạo ApiGateWay mới
            group.MapPost("/", ApiGatewayActions.CreateNewRoute)
                .WithName("CreateApiGateWay")
                .WithOpenApi();
        }
    }
}

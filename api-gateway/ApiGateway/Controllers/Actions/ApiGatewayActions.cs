using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Application.Features.Routes;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers.Actions
{
    public static class ApiGatewayActions
    {
        public static async Task<IResult> GetAllRouteInSystem(HttpContext context,
            [FromServices] GetAllRouteInSystem getAllRouteInSystem)
        {
            var result = await getAllRouteInSystem.ExecuteAsync();
            return Results.Ok(result);
        }
        
        public static async Task<IResult> CreateNewRoute(HttpContext context,
            [FromServices] CreateNewRoute createNewRoute,
            [FromBody] RouteConfigRequestDTO routeConfigRequestDTO)
        {
            var result = await createNewRoute.ExecuteAsync(routeConfigRequestDTO);
            return Results.Ok(result);
        }


    }
}

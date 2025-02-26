namespace ApiGateway.Application.DTOs.Routes
{
    public record RouteConfigRequestDTO(string Path, string TargetService, string TargetPath);
}

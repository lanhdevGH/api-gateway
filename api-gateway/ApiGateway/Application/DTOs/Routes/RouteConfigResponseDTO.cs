namespace ApiGateway.Application.DTOs.Routes
{
    public record RouteConfigResponseDTO (int Id, string Path, string TargetService, string TargetPath);
}

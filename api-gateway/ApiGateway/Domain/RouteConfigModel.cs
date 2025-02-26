using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGateway.Domain
{
    public record class RouteConfigModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; } // Trường ID tự động tăng
        public string Path { get; init; } = string.Empty; // Key
        public string TargetService { get; init; } = string.Empty;
        public string TargetPath { get; init; } = string.Empty;
    }
}

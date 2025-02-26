using ApiGateway.Application.DTOs.Routes;
using ApiGateway.Domain;
using AutoMapper;

namespace ApiGateway.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map từ Entity sang DTO
            CreateMap<RouteConfigModel, RouteConfigResponseDTO>();

            // Map ngược từ DTO sang Entity
            CreateMap<RouteConfigRequestDTO, RouteConfigModel>();
        }
    }
}

using AutoMapper;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Responses;

namespace HoursBank.CrossCutting.Mappings
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<BankEntity, BankResponse>().ReverseMap();
            CreateMap<CoordinatorEntity, CoordinatorResponse>().ReverseMap();
            CreateMap<TeamEntity, TeamResponse>().ReverseMap();
            CreateMap<TypeEntity, TypeResponse>().ReverseMap();
            CreateMap<UserEntity, UserResponse>().ReverseMap();
        }
    }
}

using AutoMapper;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;

namespace HoursBank.CrossCutting.Mappings
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<BankEntity, BankDto>().ReverseMap();
            CreateMap<CoordinatorEntity, CoordinatorDto>().ReverseMap();
            CreateMap<TeamEntity, TeamDto>().ReverseMap();
            CreateMap<TypeEntity, TypeDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}

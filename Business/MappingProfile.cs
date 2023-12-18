using AutoMapper;
using Common.Dto;
using Data.Entity;

namespace Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEntity, UserSettingsResponseDto>();
            CreateMap<TripEntity, TripResponseDto>();
        }
    }
}
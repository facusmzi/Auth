using Auth.Dtos.Session;
using Auth.Dtos.User;
using Auth.Models;
using AutoMapper;

namespace Auth.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();

            // Session mappings
            CreateMap<Session, SessionDto>();
            CreateMap<Session, SessionWithTokenDto>();
        }
    }
}

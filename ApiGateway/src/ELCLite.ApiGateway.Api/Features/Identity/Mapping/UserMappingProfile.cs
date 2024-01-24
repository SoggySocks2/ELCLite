using AutoMapper;
using ELCLite.ApiGateway.Api.Features.Identity.Models;
using ELCLite.Identity.Api.Features.Users;

namespace ELCLite.ApiGateway.Api.Features.Identity.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<RoleNameModel, RoleNameDto>().ReverseMap();


            CreateMap<CreateUserDto, CreateUserModel>();
            CreateMap<CreateUserModel, CreateUserDto>()
                .ForMember(dest => dest.RoleIds, opt => opt.Condition(src => src.RoleIds is not null));

            CreateMap<UpdateUserDto, UpdateUserModel>();
            CreateMap<UpdateUserModel, UpdateUserDto>()
                .ForMember(dest => dest.RoleIds, opt => opt.Condition(src => src.RoleIds is not null));
        }
    }
}

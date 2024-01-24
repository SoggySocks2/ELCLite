using AutoMapper;
using ELCLite.Identity.Data.Entities;

namespace ELCLite.Identity.Features.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>(MemberList.Destination)
                .ForMember(dest => dest.LanguageCode, opt => opt.MapFrom(src => src.PreferredLanguageISOCode));

            CreateMap<Role, RoleNameDto>(MemberList.Destination);

            CreateMap<CreateUserDto, User>()
                .ConvertUsing((source, dest) =>
                {
                    if (dest is not null)
                    {
                        throw new ArgumentNullException("No existing object should be provided.");
                    }

                    return new User(source.Email, source.FirstName, source.LastName, source.LanguageCode, source.IsStaff);
                });

            CreateMap<UpdateUserDto, User>()
                .ConvertUsing((source, dest) =>
                {
                    if (dest is User user)
                    {
                        user.Update(source.FirstName, source.LastName, source.LanguageCode, source.IsStaff);
                        return user;
                    }

                    throw new ArgumentNullException("No existing object is provided.");
                });
        }
    }
}

using AutoMapper;
using ELCLite.Identity.Api.Data.Entities;

namespace ELCLite.Identity.Api.Features.Roles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleDto>(MemberList.Destination);
            CreateMap<Role, RoleInfoDto>(MemberList.Destination);

            CreateMap<Permission, PermissionDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Key.Value))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Key.Description))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Key.Group));

            CreateMap<CreateRoleDto, Role>()
                .ConvertUsing((source, dest) =>
                {
                    if (dest is not null)
                    {
                        throw new ArgumentNullException("No existing object should be provided.");
                    }

                    return new Role(source.Name, source.Description);
                });

            CreateMap<UpdateRoleDto, Role>()
                .ConvertUsing((source, dest) =>
                {
                    if (dest is Role role)
                    {
                        role.Update(source.Name, source.Description);
                        return role;
                    }

                    throw new ArgumentNullException("No existing object is provided.");
                });
        }
    }
}

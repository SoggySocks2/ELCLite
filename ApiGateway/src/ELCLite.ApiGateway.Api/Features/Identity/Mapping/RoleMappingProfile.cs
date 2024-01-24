using AutoMapper;
using ELCLite.ApiGateway.Api.Features.Identity.Models;
using ELCLite.Identity.Features.Roles;

namespace ELCLite.ApiGateway.Api.Features.Identity.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleModel, RoleDto>().ReverseMap();
            CreateMap<RoleInfoModel, RoleInfoDto>().ReverseMap();
            CreateMap<PermissionModel, PermissionDto>().ReverseMap();

            CreateMap<CreateRoleDto, CreateRoleModel>();
            CreateMap<CreateRoleModel, CreateRoleDto>()
                .ForMember(dest => dest.SelectedPermissionIds, opt => opt.Condition(src => src.SelectedPermissionIds is not null));

            CreateMap<UpdateRoleDto, UpdateRoleModel>();
            CreateMap<UpdateRoleModel, UpdateRoleDto>()
                .ForMember(dest => dest.SelectedPermissionIds, opt => opt.Condition(src => src.SelectedPermissionIds is not null));
        }
    }
}

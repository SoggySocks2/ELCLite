using AutoMapper;
using ELCLite.ApiGateway.Api.Configuration.Authorization;
using ELCLite.ApiGateway.Api.Features.Identity.Contracts;
using ELCLite.ApiGateway.Api.Features.Identity.Models;
using ELCLite.Identity.Api.Features.Roles;

namespace ELCLite.ApiGateway.Api.Features.Identity.Services
{
    public class RoleProxy : IRoleProxy
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IPermissionValidator _permissionValidator;

        public RoleProxy(IMapper mapper, IRoleService roleService, IPermissionValidator permissionValidator)
        {
            _mapper = mapper;
            _roleService = roleService;
            _permissionValidator = permissionValidator;
        }

        public async Task<RoleModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<RoleModel>(roles);
        }

        public async Task<RoleModel> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetByNameAsync(name, cancellationToken);

            return _mapper.Map<RoleModel>(roles);
        }

        public async Task<List<RoleInfoModel>> GetAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetListAsync(cancellationToken);

            return _mapper.Map<List<RoleInfoModel>>(roles);
        }

        public async Task<List<RoleModel>> GetWithPermissionsAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetListWithPermissionsAsync(cancellationToken);

            return _mapper.Map<List<RoleModel>>(roles);
        }

        public async Task<RoleModel> AddAsync(CreateRoleModel createRoleModel, CancellationToken cancellationToken)
        {
            var createRoleDto = _mapper.Map<CreateRoleDto>(createRoleModel);
            var newRole = await _roleService.AddAsync(createRoleDto, cancellationToken);

            await UpdatePermissionValidator();

            return _mapper.Map<RoleModel>(newRole);
        }

        public async Task<RoleModel> UpdateAsync(UpdateRoleModel updateRoleModel, CancellationToken cancellationToken)
        {
            var updateRoleDto = _mapper.Map<UpdateRoleDto>(updateRoleModel);
            var role = await _roleService.UpdateAsync(updateRoleDto, cancellationToken);

            await UpdatePermissionValidator();

            return _mapper.Map<RoleModel>(role);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _roleService.DeleteByIdAsync(id, cancellationToken);

            await UpdatePermissionValidator();
        }

        private async Task UpdatePermissionValidator()
        {
            var roles = await GetWithPermissionsAsync(CancellationToken.None);

            _permissionValidator.UpdateRoles(roles);
        }
    }
}

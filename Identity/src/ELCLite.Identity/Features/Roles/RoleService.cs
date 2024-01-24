using AutoMapper;
using ELCLite.Identity.Data.Contracts;
using ELCLite.Identity.Data.Entities;

namespace ELCLite.Identity.Features.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IMapper mapper, IRoleRepository roleRepository)
        {
            if (mapper is null)
            {
                throw new ArgumentException($"{nameof(mapper)} is null");
            }
            if (roleRepository is null)
            {
                throw new ArgumentException($"{nameof(roleRepository)} is null");
            }

            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} is required");
            }

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

            if (role is null)
            {
                throw new ApplicationException("Role not found");
            }

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} is required");
            }

            var role = await _roleRepository.GetByNameAsync(name, cancellationToken);

            if (role is null)
            {
                throw new ApplicationException("Role not found");
            }

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<List<RoleInfoDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetListAsync(cancellationToken);

            return _mapper.Map<List<RoleInfoDto>>(roles);
        }

        public async Task<List<RoleDto>> GetListWithPermissionsAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetListWithPermissionsAsync(cancellationToken);

            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<RoleDto> AddAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken)
        {
            if (createRoleDto is null)
            {
                throw new ArgumentException($"{nameof(createRoleDto)} is required");
            }

            var newRole = _mapper.Map<Role>(createRoleDto);

            newRole.ClearAndAddPermissions(createRoleDto.SelectedPermissionIds);

            await _roleRepository.AddAsync(newRole, cancellationToken);

            return _mapper.Map<RoleDto>(newRole);
        }

        public async Task<RoleDto> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
        {
            if (updateRoleDto is null)
            {
                throw new ArgumentException($"{nameof(updateRoleDto)} is required");
            }

            if (updateRoleDto.Id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(updateRoleDto.Id)} is required");
            }

            var role = await _roleRepository.GetByIdAsync(updateRoleDto.Id, cancellationToken);

            if (role is null)
            {
                throw new ApplicationException("Role not found");
            }

            _mapper.Map<UpdateRoleDto, Role>(updateRoleDto, role);

            role.ClearAndAddPermissions(updateRoleDto.SelectedPermissionIds);

            await _roleRepository.UpdateAsync(role, cancellationToken);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} is required");
            }

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

            if (role is null)
            {
                throw new ApplicationException("Role not found");
            }

            await _roleRepository.DeleteAsync(role, cancellationToken);
        }
    }
}

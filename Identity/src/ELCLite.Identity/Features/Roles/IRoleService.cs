namespace ELCLite.Identity.Features.Roles
{
    public interface IRoleService
    {
        Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RoleDto> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<RoleInfoDto>> GetListAsync(CancellationToken cancellationToken);
        Task<List<RoleDto>> GetListWithPermissionsAsync(CancellationToken cancellationToken);
        Task<RoleDto> AddAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken);
        Task<RoleDto> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

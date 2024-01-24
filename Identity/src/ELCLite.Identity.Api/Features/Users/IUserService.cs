namespace ELCLite.Identity.Api.Features.Users
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<UserDto>> GetListAsync(CancellationToken cancellationToken);
        Task<UserDto> AddAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> TryUpdateUserInfoByEmailAsync(string email, string firstname, string lastname, CancellationToken cancellationToken);
        Task<UserDto> TryGetStaffUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}

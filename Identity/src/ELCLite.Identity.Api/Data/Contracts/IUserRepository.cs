using ELCLite.Identity.Api.Data.Entities;

namespace ELCLite.Identity.Api.Data.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetStaffByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<User>> GetListAsync(CancellationToken cancellationToken);
        Task<User> AddAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
        Task DeleteAsync(User user, CancellationToken cancellationToken);
    }
}

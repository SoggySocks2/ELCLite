using ELCLite.Identity.Data.Contracts;
using ELCLite.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELCLite.Identity.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public UserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _dbContext.Users
                .Where(x => x.Id == id)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(email)) throw new ArgumentException($"{nameof(email)} is required");

            var user = await _dbContext.Users
                .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<User> GetStaffByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(email)) throw new ArgumentException($"{nameof(email)} is required");

            var user = await _dbContext.Users
                .Where(x => x.IsStaff)
                .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        public async Task<List<User>> GetListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var count = await _dbContext.Users.CountAsync();

            var data = await _dbContext.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ToListAsync(cancellationToken);

            return data;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

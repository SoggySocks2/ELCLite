using ELCLite.Identity.Api.Data.Contracts;
using ELCLite.Identity.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ELCLite.Identity.Api.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _dbContext;

        public RoleRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _dbContext.Roles
                .Where(x => x.Id == id)
                .Include(x => x.RolePermissions)
                .FirstOrDefaultAsync(cancellationToken);

            return role;
        }

        public async Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} is required");

            var role = await _dbContext.Roles
                .Where(x => x.NormalizedName == name.Normalize().ToUpperInvariant())
                .Include(x => x.RolePermissions)
                .FirstOrDefaultAsync(cancellationToken);

            return role;
        }

        public async Task<Role> GetByIdWithUsersAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _dbContext.Roles
                .Where(x => x.Id == id)
                .Include(x => x.RolePermissions)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(cancellationToken);

            return role;
        }

        public async Task<Role> GetByNameWithUsersAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} is required");

            var role = await _dbContext.Roles
                .Where(x => x.NormalizedName == name.Normalize().ToUpperInvariant())
                .Include(x => x.RolePermissions)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(cancellationToken);

            return role;
        }

        public async Task<List<Role>> GetListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var roles = await _dbContext.Roles
                .ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<List<Role>> GetListByIdAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var roles = await _dbContext.Roles
                .Where(c => ids.Contains(c.Id))
                .ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<List<Role>> GetListWithPermissionsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var roles = await _dbContext.Roles
            .Include(x => x.RolePermissions)
            .ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<Role> AddAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return role;
        }
        public async Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return role;
        }

        public async Task DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

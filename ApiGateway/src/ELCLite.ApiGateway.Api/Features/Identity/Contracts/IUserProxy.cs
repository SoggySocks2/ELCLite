using ELCLite.ApiGateway.Api.Features.Identity.Models;

namespace ELCLite.ApiGateway.Api.Features.Identity.Contracts
{
    public interface IUserProxy
    {
        Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<UserModel>> GetAsync(CancellationToken cancellationToken);
        Task<UserModel> AddAsync(CreateUserModel createUserModel, CancellationToken cancellationToken);
        Task<UserModel> UpdateAsync(UpdateUserModel updateUserModel, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}

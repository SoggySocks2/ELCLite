using ELCLite.Identity.Models.Identity;

namespace ELCLite.Identity.Infrastructure.Identities
{
    public interface IIdentityRepository
    {
        Task<UserModel> AddAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken);
        Task<UserAuthenicatedModel> LoginAsync(UserLoginModel userLoginModel, CancellationToken cancellationToken);
        Task<bool> AddPasswordAsync(AddPasswordModel addPasswordModel, CancellationToken cancellationToken);
        Task<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel, CancellationToken cancellationToken);
        Task LogoutAsync(CancellationToken cancellationToken);
    }
}

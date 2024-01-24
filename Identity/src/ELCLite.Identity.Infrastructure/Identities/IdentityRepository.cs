using ELCLite.Identity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELCLite.Identity.Infrastructure.Identities
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public IdentityRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public async Task<UserModel> AddAsync(RegisterUserModel registerUserModel, CancellationToken cancellationToken)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, registerUserModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, registerUserModel.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, registerUserModel.Password);

            var userModel = new UserModel();
            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                userModel.Id = userId;
                userModel.Email = registerUserModel.Email;
                userModel.TelNo = registerUserModel.TelNo;
            }

            return userModel;
        }

        public async Task<UserAuthenicatedModel> LoginAsync(UserLoginModel userLoginModel, CancellationToken cancellationToken)
        {
            var userAuthenicatedModel = new UserAuthenicatedModel();
            var result = await _signInManager.PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, userLoginModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userLoginModel.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    foreach (var userClaim in userClaims)
                    {
                        authClaims.Add(new Claim(userClaim.Type, userClaim.Value));
                    }

                    var claimsIdentity = new ClaimsIdentity();
                    foreach (var claim in authClaims)
                    {
                        claimsIdentity.AddClaim(claim);
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    //var encryptionKey = configuration.GetValue<string>("JWTEncryptionKey");
                    var key = Encoding.ASCII.GetBytes("My JWT Encryption Key My JWT Encryption Key");

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    userAuthenicatedModel.Email = user.Email ?? string.Empty;
                    userAuthenicatedModel.Token = tokenString;
                }
            }

            return userAuthenicatedModel;
        }

        public async Task<UserAuthenicatedModel> LoginAsync_BK(UserLoginModel userLoginModel, CancellationToken cancellationToken)
        {
            var userAuthenicatedModel = new UserAuthenicatedModel();
            var result = await _signInManager.PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, userLoginModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userLoginModel.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }


                    var token = await _userManager.GenerateUserTokenAsync(user, "Default", "Login");

                    userAuthenicatedModel.Email = user.Email ?? string.Empty;
                    userAuthenicatedModel.Token = token;
                }
            }

            return userAuthenicatedModel;
        }

        public async Task<bool> AddPasswordAsync(AddPasswordModel addPasswordModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(addPasswordModel.Email);
            if (user == null)
            {
                return false;
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, addPasswordModel.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                return false;
            }

            await _signInManager.RefreshSignInAsync(user);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordModel resetPasswordModel, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result = await _userManager.ResetPasswordAsync(user, code, resetPasswordModel.NewPassword);

            return result.Succeeded;
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        public async Task LogoutAsync(CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }
    }
}

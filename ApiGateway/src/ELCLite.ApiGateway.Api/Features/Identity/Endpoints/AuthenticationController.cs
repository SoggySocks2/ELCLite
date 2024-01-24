using ELCLite.ApiGateway.Api.Features.Identity.Models;
using ELCLite.SharedKernel.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELCLite.ApiGateway.Api.Features.Identity.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private IIdentitySettings _identitySettings { get; }

        public AuthenticationController(IIdentitySettings identitySettings)
        {
            _identitySettings = identitySettings;
        }

        /// <summary>
        /// Authenticate and generate a new JWT
        /// </summary>
        /// <returns>JWT</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTokenAsync([FromBody] AuthenticationModel authenticationModel, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (authenticationModel == null) return BadRequest($"{nameof(authenticationModel)} is required");

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_identitySettings.EncryptionKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, "123"),
                            new Claim(ClaimTypes.Name, $"Peter Jones"),
                            new Claim(ClaimTypes.Role, "GlobalAdmin")
                            //new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}

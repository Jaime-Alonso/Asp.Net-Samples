using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi_JWT_Security.Configurations;
using WebApi_JWT_Security.DTOs.User;

namespace WebApi_JWT_Security.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AuthenticationSettings> authenticationSettings)
        {
            _authenticationSettings = authenticationSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<TokenResponse> SignInAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            return !result.Succeeded ? null : new TokenResponse { Token = GenerateSecurityToken(request) };
        }

        private string GenerateSecurityToken(LoginRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, request.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(_authenticationSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

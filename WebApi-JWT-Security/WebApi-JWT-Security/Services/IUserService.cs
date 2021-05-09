using System.Threading.Tasks;
using WebApi_JWT_Security.DTOs.User;

namespace WebApi_JWT_Security.Services
{
    public interface IUserService
    {
        Task<TokenResponse> SignInAsync(LoginRequest request);
    }
}
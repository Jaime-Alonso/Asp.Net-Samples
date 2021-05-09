using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi_JWT_Security.DTOs.User;
using WebApi_JWT_Security.Services;

namespace WebApi_JWT_Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginAsync(LoginRequest model)
        {
            var token = await _userService.SignInAsync(model);

            if (token == null) return Unauthorized();

            return Ok(token);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApi_JWT_Security.DTOs.User
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

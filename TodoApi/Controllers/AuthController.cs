using Microsoft.AspNetCore.Mvc;
using TodoApi.Services.Contracts;
using TodoApi.Services.Core;
using static TodoApi.Services.Dtos.UserDto;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserAddRequestDto model)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = passwordHash;
            var user = _authService.Register(model);

            if (user == null)
                return BadRequest(new { message = "Email is already taken" });

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto model)
        {
            var user = _authService.Authenticate(model.Email, model.Password);

            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            var token = _authService.GenerateJwtToken(user);

            return Ok(new LoginResponseDto { Token = token });
        }
    }
}

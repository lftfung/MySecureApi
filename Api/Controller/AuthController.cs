using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MySecureApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto) { 
            var result = await _authService.Register(dto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) { 
            var result = await _authService.Login(dto);

            if (result.StartsWith("eyJ")) {
                return Ok(new { 
                    message = "Login successful",
                    token = result
                });
            }

            return BadRequest(new {message = result });
        }
    }
}

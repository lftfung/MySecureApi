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
           
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) { 
            var result = await _authService.Login(dto);

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

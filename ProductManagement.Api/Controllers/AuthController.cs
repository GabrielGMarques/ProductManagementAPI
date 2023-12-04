using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Dtos.Auth;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(int?), 400)]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {
            var result = await _authService.Register(userDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var result = await _authService.Login(userDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }
      
    }
}

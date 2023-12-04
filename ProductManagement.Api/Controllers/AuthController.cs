using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Common;
using ProductManagement.Domain.Contracts.Services;
using ProductManagement.Domain.Shared.Dtos;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
            try
            {
                var result = await _authService.Register(userDto);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error regstering the user {ex.Message}");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            try
            {
                var result = await _authService.Login(userDto);

                if (result.Success)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error signin the user {ex.Message}");
                return StatusCode(500, ErrorMessages.UnhandledException);
            }
        }

    }
}

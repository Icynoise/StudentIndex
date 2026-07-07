using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;

namespace StudentIndex.Server.WebApi
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
        public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result.Succeeded)
                return Ok(new { message = "Korisnik je uspješno registrovan." });
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            if (result.Succeeded)
                return Ok(new { token = result.Token, refreshToken = result.RefreshToken });
            return Unauthorized(result.Errors);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(new { token = result.Token, refreshToken = result.RefreshToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            await _authService.LogoutAsync(request.RefreshToken);
            return Ok(new { message = "Uspješna odjava." });
        }
    }
}

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

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
                return Ok("User created with role: " + request.Role);
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            if (result.Succeeded)
                return Ok(new { token = result.Token });
            return Unauthorized(result.Errors);
        }
    }
}

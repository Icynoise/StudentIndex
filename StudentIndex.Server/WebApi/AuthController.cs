using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(string email, string password, string role, string ime, string prezime, string emailStudent, string telefon, DateOnly datumRodjenja, string status)
        {
            var result = await _authService.RegisterAsync(email, password, role, ime, prezime, emailStudent, telefon, datumRodjenja, status);
            if (result.Succeeded)
                return Ok("User created with role: " + role);
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
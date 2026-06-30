using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;
using StudentIndex.Server.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentIndex.Server.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStudentRepository _studentRepository;
        private readonly string _jwtSecret;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IStudentRepository studentRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
            _jwtSecret = configuration["JwtSettings:Secret"]
                ?? throw new ArgumentNullException(nameof(configuration), "JWT Secret is missing in configuration.");
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterStudentRequest request)
        {
            if (!await _roleManager.RoleExistsAsync(request.Role))
                await _roleManager.CreateAsync(new IdentityRole(request.Role));

            var student = new Studenti
            {
                Ime = request.Ime,
                Prezime = request.Prezime,
                Email = request.EmailStudent,
                Telefon = request.Telefon,
                DatumRođenja = request.DatumRodjenja,
                Status = request.Status
            };

            await _studentRepository.AddAsync(student);

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                StudentId = student.StudentId
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role);
                return new AuthResultDto { Succeeded = true };
            }

            return new AuthResultDto { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
        }

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("StudentId", user.StudentId?.ToString() ?? string.Empty)
                };
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                return new AuthResultDto
                {
                    Succeeded = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }

            return new AuthResultDto { Succeeded = false, Errors = new[] { "Invalid email or password" } };
        }
    }
}

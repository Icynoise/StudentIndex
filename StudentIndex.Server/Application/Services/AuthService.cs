using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;
using StudentIndex.Server.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentIndex.Server.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _jwtSecret;
        private readonly StudentAplikacijaContext _context;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, StudentAplikacijaContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSecret = configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException(nameof(configuration), "JWT Secret is missing in configuration.");
            _context = context;
        }

        public async Task<AuthResultDto> RegisterAsync(string email, string password, string role, string ime, string prezime, string emailStudent, string telefon, DateOnly datumRodjenja, string status)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            var student = new Studenti
            {
                Ime = ime,
                Prezime = prezime,
                Email = emailStudent,
                Telefon = telefon,
                DatumRođenja = datumRodjenja,
                Status = status
            };

            _context.Studenti.Add(student);
            await _context.SaveChangesAsync();

            var user = new ApplicationUser { UserName = email, Email = email, StudentId = student.StudentId };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
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
                    new Claim("StudentId", user.StudentId.ToString())
                };
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
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
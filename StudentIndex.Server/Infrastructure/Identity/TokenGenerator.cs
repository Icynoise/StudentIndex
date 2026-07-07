using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentIndex.Server.Infrastructure.Identity;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtSettings _settings;

    public TokenGenerator(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }

    public string GenerateAccessToken(UserInfoDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.UserId),
            new(CustomClaimTypes.StudentId, user.StudentId?.ToString() ?? string.Empty)
        };
        claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}

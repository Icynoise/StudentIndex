using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace StudentIndex.Server.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly StudentAplikacijaContext _context;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        StudentAplikacijaContext context,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _context = context;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateStudentUserAsync(
        string email, string password, int studentId)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            StudentId = studentId
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description).ToList());

        // Rola je uvijek Student — klijent je ne može birati.
        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Student);
        if (!roleResult.Succeeded)
            return (false, roleResult.Errors.Select(e => e.Description).ToList());

        return (true, Enumerable.Empty<string>());
    }

    public async Task<UserInfoDto?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            return null;

        return await ToUserInfoAsync(user);
    }

    public async Task StoreRefreshTokenAsync(string userId, string refreshToken)
    {
        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = userId,
            TokenHash = Hash(refreshToken),
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays)
        });
        await _context.SaveChangesAsync();
    }

    public async Task<UserInfoDto?> ValidateAndRevokeRefreshTokenAsync(string refreshToken)
    {
        var stored = await FindByTokenAsync(refreshToken);
        if (stored == null || stored.RevokedAtUtc != null || stored.ExpiresAtUtc < DateTime.UtcNow)
            return null;

        // Rotacija: iskorišćeni token se odmah opoziva.
        stored.RevokedAtUtc = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return await ToUserInfoAsync(stored.User);
    }

    public async Task RevokeAllRefreshTokensAsync(string userId)
    {
        await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.RevokedAtUtc == null)
            .ExecuteUpdateAsync(s => s.SetProperty(rt => rt.RevokedAtUtc, DateTime.UtcNow));
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var stored = await FindByTokenAsync(refreshToken);
        if (stored == null || stored.RevokedAtUtc != null)
            return;

        stored.RevokedAtUtc = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    private Task<RefreshToken?> FindByTokenAsync(string refreshToken)
    {
        var hash = Hash(refreshToken);
        return _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == hash);
    }

    private async Task<UserInfoDto> ToUserInfoAsync(ApplicationUser user) => new()
    {
        UserId = user.Id,
        Email = user.Email!,
        StudentId = user.StudentId,
        Roles = await _userManager.GetRolesAsync(user)
    };

    private static string Hash(string token)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}

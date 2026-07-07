using StudentIndex.Server.Application.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    /// <summary>
    /// Apstrakcija nad ASP.NET Identity — Application sloj ne zavisi od
    /// UserManager/ApplicationUser tipova iz Infrastructure sloja.
    /// </summary>
    public interface IIdentityService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateStudentUserAsync(string email, string password, int studentId);

        /// <summary>Vraća podatke o korisniku ako su kredencijali ispravni, inače null.</summary>
        Task<UserInfoDto?> ValidateCredentialsAsync(string email, string password);

        Task StoreRefreshTokenAsync(string userId, string refreshToken);

        /// <summary>
        /// Validira refresh token i odmah ga opoziva (rotacija).
        /// Vraća korisnika ako je token važeći, inače null.
        /// </summary>
        Task<UserInfoDto?> ValidateAndRevokeRefreshTokenAsync(string refreshToken);

        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}

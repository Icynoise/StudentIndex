using StudentIndex.Server.Application.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    /// <summary>
    /// Apstrakcija za izdavanje tokena — JWT detalji žive u Infrastructure sloju.
    /// </summary>
    public interface ITokenGenerator
    {
        string GenerateAccessToken(UserInfoDto user);
        string GenerateRefreshToken();
    }
}

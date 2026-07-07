using StudentIndex.Server.Application.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterStudentRequest request);
        Task<AuthResultDto> LoginAsync(string email, string password);
        Task<AuthResultDto> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}

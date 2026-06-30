using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterStudentRequest request);
        Task<AuthResultDto> LoginAsync(string email, string password);
    }
}

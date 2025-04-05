using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(string email, string password, string role, string ime, string prezime, string emailStudent, string telefon, DateOnly datumRodjenja, string status);
        Task<AuthResultDto> LoginAsync(string email, string password);
    }

}

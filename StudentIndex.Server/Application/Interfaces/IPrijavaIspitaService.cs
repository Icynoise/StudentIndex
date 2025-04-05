using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPrijavaIspitaService
    {
        Task<PrijavaIspitaDto> GetStudentExamRegistrationDataAsync(int userId);
        Task<List<DostupniIspitiDto>> GetAvailableExamsAsync(int userId);
        Task RegisterForExamAsync(int userId, int ispitId);
    }
}

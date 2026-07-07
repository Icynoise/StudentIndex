using StudentIndex.Server.Application.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPrijavaIspitaService
    {
        Task<PrijavaIspitaDto> GetStudentExamRegistrationDataAsync(int userId);

        /// <summary>Vraća kompozabilan upit dostupnih ispita — kontroler ga izvršava kroz SmartResult.</summary>
        Task<IQueryable<DostupniIspitiDto>> GetAvailableExamsQueryAsync(int userId);

        Task RegisterForExamAsync(int userId, int ispitId);
    }
}

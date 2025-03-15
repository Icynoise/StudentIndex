// StudentIndex.Application/Interfaces/IStudentService.cs
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<PredmetiDto>> GetStudentSubjectsAsync(int studentId, int yearId, int semesterId);
    }
}
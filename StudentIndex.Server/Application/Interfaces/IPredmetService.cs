// StudentIndex.Application/Interfaces/IStudentService.cs
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Application.Interfaces
{
    public interface IPredmetService
    {
        Task<IEnumerable<PredmetiDto>> GetStudentPredmetiPoSemestru(int studentId, int semesterId);
    }
}
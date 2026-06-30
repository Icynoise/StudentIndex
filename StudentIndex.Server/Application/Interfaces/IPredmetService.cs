using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPredmetService
    {
        Task<IEnumerable<PredmetiDto>> GetStudentPredmetiPoSemestru(int studentId, int semesterId);
    }
}

using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPredmetRepository
    {
        Task<IEnumerable<Predmeti>> GetStudentPredmetiPoSemestru(int studentId, int semesterId);
       
    }
}
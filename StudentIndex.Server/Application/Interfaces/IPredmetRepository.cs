using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPredmetRepository
    {
        Task<IEnumerable<Predmeti>> GetStudentPredmetiPoSemestru(int studentId, int semesterId);
       
    }
}
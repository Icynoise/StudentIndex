using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Studenti?> GetByStudentIdAsync(int studentId);
        Task AddAsync(Studenti student);
    }
}

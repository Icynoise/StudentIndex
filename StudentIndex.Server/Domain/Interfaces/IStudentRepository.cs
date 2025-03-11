using System.Collections.Generic;
using System.Threading.Tasks;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Domain;
namespace Core.Interfaces
{
    public interface IStudentRepository
    {
        // Get a student by ID
        Task<Studenti> GetStudentByIdAsync(int id);

        // Get all students
        Task<IEnumerable<Studenti>> GetAllStudentsAsync();

        // Add a new student
        Task AddStudentAsync(Studenti student);

        // Update an existing student
        Task UpdateStudentAsync(Studenti student);

        // Delete a student by ID
        Task DeleteStudentAsync(int id);

        Task<IEnumerable<PredmetiDto>> GetStudentSubjectsByYearAndSemesterAsync(
        int studentId, int studijskiProgramId, int semestarId);
    }
}

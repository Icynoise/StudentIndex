using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
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
     
        Task<IEnumerable<PredmetiDto>> GetStudentSubjectsAsync(int studentId, int yearId, int semesterId);
       
    }
}
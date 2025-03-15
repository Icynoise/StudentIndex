// StudentIndex.Application/Services/StudentService.cs
using StudentIndex.Application.Interfaces;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<PredmetiDto>> GetStudentSubjectsAsync(int studentId, int yearId, int semesterId)
        {
            // Delegate to the repository (data access layer)
            return await _studentRepository.GetStudentSubjectsAsync(studentId, yearId, semesterId);
        }
    }
}
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Services
{
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepositroy) {

            _studentRepo = studentRepositroy;

        }

        public async Task<StudentDto> GetByUserId(int userId)
        {
            var student = await _studentRepo.GetByUserId(userId);

            return new StudentDto
            {
                Ime = student.Ime,
                Prezime = student.Prezime,
                NazivStudijskiProgram = student.StudentStudijskiPrograms
                .FirstOrDefault()?.StudijskiProgram?.Naziv ?? "N/A" // Handle null case safely
            };
        }
    }
}

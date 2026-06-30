using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepo = studentRepository;
        }

        public async Task<StudentDto> GetByUserId(int userId)
        {
            var student = await _studentRepo.GetByStudentIdAsync(userId);
            if (student == null)
                throw new Exception("Student not found");

            return new StudentDto
            {
                Ime = student.Ime,
                Prezime = student.Prezime,
                NazivStudijskiProgram = student.StudentStudijskiPrograms
                    .FirstOrDefault()?.StudijskiProgram?.Naziv ?? "N/A"
            };
        }
    }
}

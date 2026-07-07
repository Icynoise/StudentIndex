using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Exceptions;
using StudentIndex.Server.Application.Interfaces;

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
                throw new NotFoundException("Student nije pronađen.");

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

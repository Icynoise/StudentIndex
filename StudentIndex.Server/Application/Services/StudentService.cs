using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Infrastructure.Repositories;

namespace StudentIndex.Server.Application.Services
{
    public class StudentService
    {
        StudentRepository _studentRepository;
        private readonly IMapper _mapper;

        StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        // Application/Services/StudentService.cs
        public async Task<IEnumerable<PredmetiDto>> GetStudentSubjectsByYearAndSemesterAsync(
            int studentId, int yearId, int semesterId)
        {
            // Validate input parameters
            if (studentId <= 0 || yearId <= 0 || semesterId <= 0)
                throw new ArgumentException("Invalid parameters");

            // Get student subjects using repository
            var subjects = await _studentRepository.GetStudentSubjectsByYearAndSemesterAsync(
                studentId, yearId, semesterId);

            // Map domain entities to DTOs
            return subjects;
        }

    }
}

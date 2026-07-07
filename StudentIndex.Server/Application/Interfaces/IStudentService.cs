using StudentIndex.Server.Application.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<StudentDto> GetByUserId(int userId);
    }
}

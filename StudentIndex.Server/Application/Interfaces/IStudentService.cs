using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<StudentDto> GetByUserId(int userId);
    }
}

using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IStudentRepository
    {
        public Task<Studenti> GetByUserId(int userId);
    }
}

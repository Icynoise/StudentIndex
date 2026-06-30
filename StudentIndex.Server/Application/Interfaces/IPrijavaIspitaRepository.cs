using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IPrijavaIspitaRepository
    {
        Task AddAsync(StudentIspiti studentIspit);
        Task<bool> ExistsPendingRegistrationAsync(int studentId, int ispitId);
    }
}

using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IIspitRepository
    {
        Task<Ispiti> GetByIdAsync(int ispitId);
        Task<List<Ispiti>> GetAvailableExamsForProgramAsync(int studijskiProgramId);
    }
}

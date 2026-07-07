using StudentIndex.Server.Domain;

namespace StudentIndex.Server.Application.Interfaces
{
    public interface IIspitRepository
    {
        Task<Ispiti?> GetByIdAsync(int ispitId);

        /// <summary>Kompozabilan upit — SmartResult na njega dodaje filtere/sort/paging prije izvršenja.</summary>
        IQueryable<Ispiti> QueryAvailableExamsForProgram(int studijskiProgramId);
    }
}

using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Infrastructure.Data;

namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class IspitiRepository : IIspitRepository
    {
        private readonly StudentAplikacijaContext _context;

        public IspitiRepository(StudentAplikacijaContext context)
        {
            _context = context;
        }

        public async Task<Ispiti?> GetByIdAsync(int ispitId)
        {
            return await _context.Ispiti
                .Include(i => i.Predmet)
                .FirstOrDefaultAsync(i => i.IspitId == ispitId);
        }

        public IQueryable<Ispiti> QueryAvailableExamsForProgram(int studijskiProgramId)
        {
            return _context.Ispiti
                .Where(i => _context.PredmetiUprogramima
                    .Any(pup => pup.StudijskiProgramId == studijskiProgramId && pup.PredmetId == i.PredmetId));
        }
    }
}

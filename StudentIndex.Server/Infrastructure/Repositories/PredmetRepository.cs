using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Infrastructure.Data;


namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class PredmetRepository : IPredmetRepository
    {
        private readonly StudentAplikacijaContext _context;

        public PredmetRepository(StudentAplikacijaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Predmeti>> GetStudentPredmetiPoSemestru(int studentId, int semesterId)
        {
            // Get the latest exam attempt results for each exam taken by this student
            var latestStudentIspiti = await _context.StudentIspiti
                .Where(si => si.StudentId == studentId)
                .GroupBy(si => si.IspitId)
                .Select(g => g.OrderByDescending(si => si.Student).FirstOrDefault())
                .ToListAsync();

            // Query to get student subjects
            var query = from s in _context.Studenti
                        join ssp in _context.StudentStudijskiProgram on s.StudentId equals ssp.StudentId into ssp_join
                        from ssp in ssp_join.DefaultIfEmpty()
                        join pup in _context.PredmetiUprogramima on ssp.StudijskiProgramId equals pup.StudijskiProgramId into pup_join
                        from pup in pup_join.DefaultIfEmpty()
                        join p in _context.Predmeti on pup.PredmetId equals p.PredmetId into p_join
                        from p in p_join.DefaultIfEmpty()
                        join se in _context.Semestri on pup.SemestarId equals se.SemestarId into se_join
                        from se in se_join.DefaultIfEmpty()
                        join i in _context.Ispiti on p.PredmetId equals i.PredmetId into i_join
                        from i in i_join.DefaultIfEmpty()
                        where s.StudentId == studentId && se.SemestarId == semesterId
                        orderby p.Naziv
                        select new
                        {
                            p.PredmetId,
                            p.Naziv,
                            p.Ects,
                            IspitId = (int?)i.IspitId
                        };

            var result = await query.ToListAsync();

            // Combine subjects with the latest exam results
            return result.Select(r => new Predmeti
            {
                PredmetId = r.PredmetId,
                Naziv = r.Naziv,
                Ects = r.Ects,
                RezultatIspita = latestStudentIspiti
            .FirstOrDefault(si => si.IspitId == r.IspitId && si.StudentId == studentId)?.RezultatIspita
            });
        }

    }
}
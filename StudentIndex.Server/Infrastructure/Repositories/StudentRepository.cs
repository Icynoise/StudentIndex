using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Infrastructure.Data;

namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAplikacijaContext _context;

        public StudentRepository(StudentAplikacijaContext context)
        {
            _context = context;
        }

        // Get a student by ID
        public async Task<Studenti> GetStudentByIdAsync(int id)
        {
            return await _context.Studenti
                .Include(s => s.StudentIspitis) // Include related data
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        // Get all students
        public async Task<IEnumerable<Studenti>> GetAllStudentsAsync()
        {
            return await _context.Studenti
                .Include(s => s.StudentIspitis) // Include related data
                .ToListAsync();
        }

        // Add a new student
        public async Task AddStudentAsync(Studenti student)
        {
            _context.Studenti.Add(student);
            await _context.SaveChangesAsync();
        }

        // Update an existing student
        public async Task UpdateStudentAsync(Studenti student)
        {
            _context.Studenti.Update(student);
            await _context.SaveChangesAsync();
        }

        // Delete a student by ID
        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Studenti.FindAsync(id);
            if (student != null)
            {
                _context.Studenti.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<PredmetiDto>> GetStudentSubjectsByYearAndSemesterAsync(int studentId, int studijskiProgramId, int semestarId)
        {
            // Get the latest exam result for each subject
            var latestExamResults = _context.StudentIspiti
                .Where(si => si.StudentId == studentId)
                .GroupBy(si => si.IspitId)
                .Select(g => g.OrderByDescending(si => si.Pokušaji).FirstOrDefault());

            // Main query
            var query = from s in _context.Studenti
                        join ssp in _context.StudentStudijskiProgram
                            on s.StudentId equals ssp.StudentId
                        join pup in _context.PredmetiUprogramima
                            on ssp.StudijskiProgramId equals pup.StudijskiProgramId
                        join p in _context.Predmeti
                            on pup.PredmetId equals p.PredmetId
                        join se in _context.Semestri
                            on pup.SemestarId equals se.SemestarId
                        join i in _context.Ispiti
                            on p.PredmetId equals i.PredmetId into examGroup
                        from i in examGroup.DefaultIfEmpty()
                            // Left join with our latest exam results
                        join si in latestExamResults
                            on new { IspitId = i.IspitId, StudentId = s.StudentId } equals
                               new { IspitId = si.IspitId, StudentId = si.StudentId } into examResults
                        from si in examResults.DefaultIfEmpty()
                        where s.StudentId == studentId
                            && ssp.StudijskiProgramId == studijskiProgramId
                            && se.SemestarId == semestarId
                        orderby p.Naziv
                        select new StudentSubjectWithStatus
                        {
                            PredmetId = p.PredmetId,
                            Naziv = p.Naziv,
                            ECTS = p.ECTS,
                            Status = si == null ? "Nema izlazaka na ispit" :
                                     si.RezultatIspita > 5 ? "Polozeno" : "Nepolozeno"
                        };

            return await query.ToListAsync();
        }
    }
}

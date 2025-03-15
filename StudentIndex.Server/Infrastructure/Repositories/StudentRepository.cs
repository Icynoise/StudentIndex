using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;
using StudentIndex.Server.Infrastructure.Data;
using System.Runtime.CompilerServices;

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

        public Task DeleteStudentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PredmetiDto>> GetStudentSubjectsAsync(int studentId, int yearId, int semesterId)
        {
            var query = @"
SELECT 
    p.PredmetId,
    p.Naziv,
    p.ECTS,
    CASE 
        WHEN si.RezultatIspita IS NULL THEN 'Nema izlazaka na ispit'
        WHEN si.RezultatIspita > 5 THEN 'Polozeno'
        ELSE 'Nepolozeno'
    END AS Status
FROM 
    dbo.Studenti s
    LEFT JOIN dbo.StudentStudijskiProgram ssp ON ssp.StudentId = s.StudentId
    LEFT JOIN dbo.PredmetiUProgramima pup ON pup.StudijskiProgramId = ssp.StudijskiProgramId
    LEFT JOIN dbo.Predmeti p ON pup.PredmetId = p.PredmetId
    LEFT JOIN dbo.Semestri se ON se.SemestarId = pup.SemestarId
    LEFT JOIN dbo.Ispiti i ON i.PredmetId = p.PredmetId
    LEFT JOIN (
        SELECT 
            StudentId, 
            IspitId, 
            RezultatIspita,
            ROW_NUMBER() OVER (PARTITION BY IspitId ORDER BY [Pokušaji] DESC) AS rn
        FROM dbo.StudentIspiti
        WHERE StudentId = @p0
    ) si ON si.IspitId = i.IspitId AND si.StudentId = s.StudentId AND si.rn = 1
WHERE 
    s.StudentId = @p0
    AND se.SemestarId = @p1
ORDER BY 
    p.Naziv";

            return await _context.Database
                .SqlQuery<PredmetiDto>(FormattableStringFactory.Create(query, studentId, semesterId))
                .ToListAsync();
        }
    }
}
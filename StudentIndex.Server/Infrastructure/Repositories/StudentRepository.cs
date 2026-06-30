using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
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

        public async Task<Studenti?> GetByStudentIdAsync(int studentId)
        {
            return await _context.Studenti
                .Include(s => s.StudentStudijskiPrograms)
                .ThenInclude(ssp => ssp.StudijskiProgram)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task AddAsync(Studenti student)
        {
            _context.Studenti.Add(student);
            await _context.SaveChangesAsync();
        }
    }
}

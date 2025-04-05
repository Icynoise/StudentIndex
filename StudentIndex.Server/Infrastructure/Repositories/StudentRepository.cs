using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Infrastructure.Data;

namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        StudentAplikacijaContext _context;
        public StudentRepository(StudentAplikacijaContext context) {
            _context = context;
        }

        public async Task<Studenti?> GetByUserId(int userId)
        {
            return await _context.Studenti
            .Include(s => s.StudentStudijskiPrograms)
            .ThenInclude(ssp => ssp.StudijskiProgram)
            .FirstOrDefaultAsync(s => s.StudentId == userId);
        }
    }
}

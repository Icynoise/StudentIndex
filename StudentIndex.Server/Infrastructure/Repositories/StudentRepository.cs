using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Core;
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
    }
}

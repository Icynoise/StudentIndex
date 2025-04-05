using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Infrastructure.Data;

namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class PrijavaIspitaRepository : IPrijavaIspitaRepository
    {
        private readonly StudentAplikacijaContext _context;

        public PrijavaIspitaRepository(StudentAplikacijaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StudentIspiti studentIspit)
        {
            _context.StudentIspiti.Add(studentIspit);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsPendingRegistrationAsync(int studentId, int ispitId)
        {
            return await _context.StudentIspiti
                .AnyAsync(si => si.StudentId == studentId
                             && si.IspitId == ispitId
                             && si.Status == "Na Cekanju");
        }
    }
}

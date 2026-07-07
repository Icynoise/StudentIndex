using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Application.Exceptions;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Infrastructure.Data;

namespace StudentIndex.Server.Infrastructure.Repositories
{
    public class PrijavaIspitaRepository : IPrijavaIspitaRepository
    {
        // SQL Server kodovi za povredu unique indexa/constrainta
        private const int UniqueIndexViolation = 2601;
        private const int UniqueConstraintViolation = 2627;

        private readonly StudentAplikacijaContext _context;

        public PrijavaIspitaRepository(StudentAplikacijaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StudentIspiti studentIspit)
        {
            _context.StudentIspiti.Add(studentIspit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                throw new ConflictException("Ispit je već prijavljen i čeka odobrenje.");
            }
        }

        public async Task<bool> ExistsPendingRegistrationAsync(int studentId, int ispitId)
        {
            return await _context.StudentIspiti
                .AnyAsync(si => si.StudentId == studentId
                             && si.IspitId == ispitId
                             && si.Status == StatusIspita.NaCekanju);
        }

        private static bool IsUniqueViolation(DbUpdateException ex)
            => ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
               && (sqlEx.Number == UniqueIndexViolation || sqlEx.Number == UniqueConstraintViolation);
    }
}

using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;

namespace StudentIndex.Server.Application.Services
{
    public class PredmetService : IPredmetService
    {
        private readonly IPredmetRepository _predmetRepository;

        public PredmetService(IPredmetRepository predmetRepository)
        {
            _predmetRepository = predmetRepository;
        }

        public async Task<IEnumerable<PredmetiDto>> GetStudentPredmetiPoSemestru(int studentId, int semesterId)
        {
            var subjects = await _predmetRepository.GetStudentPredmetiPoSemestru(studentId, semesterId);
            return subjects.Select(s => new PredmetiDto
            {
                Naziv = s.Naziv,
                Ects = s.Ects,
                RezultatIspita = s.RezultatIspita,
                Status = GetStatus(s.RezultatIspita)
            }).ToList();
        }

        private static string GetStatus(int? rezultatIspita)
        {
            if (rezultatIspita == null)
                return StatusIspita.NemaIzlazaka;

            return rezultatIspita > 5 ? StatusIspita.Polozeno : StatusIspita.Nepolozeno;
        }
    }
}

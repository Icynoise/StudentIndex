using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Domain.DTOs;

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

        private static string GetStatus(string? rezultatIspita)
        {
            if (string.IsNullOrEmpty(rezultatIspita))
                return StatusIspita.NemaIzlazaka;

            if (int.TryParse(rezultatIspita, out int rezultat))
                return rezultat > 5 ? StatusIspita.Polozeno : StatusIspita.Nepolozeno;

            return StatusIspita.NevalidanUnos;
        }
    }
}

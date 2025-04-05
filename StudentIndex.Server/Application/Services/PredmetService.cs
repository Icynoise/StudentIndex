using StudentIndex.Application.Interfaces;
using StudentIndex.Server.Application.Interfaces;

using StudentIndex.Server.Domain.DTOs;


namespace StudentIndex.Application.Services
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
            //try catch

           
                // Delegate to the repository (data access layer)
                var subjects = await _predmetRepository.GetStudentPredmetiPoSemestru(studentId, semesterId);
                return subjects.Select(s => new PredmetiDto
                {
                    Naziv = s.Naziv,
                    Ects = s.Ects,
                    Status = s.GetStatus(),
                    RezultatIspita = s.RezultatIspita,

                }).ToList();
         

          
        }
    }
}
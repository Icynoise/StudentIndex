using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.Application.Services
{
    public class PrijavaIspitaService : IPrijavaIspitaService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IIspitRepository _ispitRepository;
        private readonly IPrijavaIspitaRepository _prijavaIspitaRepository;
        public PrijavaIspitaService(IStudentRepository studentRepository, IIspitRepository ispitRepository, IPrijavaIspitaRepository prijavaIspitaRepository
            )
        {
            _studentRepository = studentRepository;
            _ispitRepository = ispitRepository;
            _prijavaIspitaRepository = prijavaIspitaRepository;
        }

        public async Task<List<DostupniIspitiDto>> GetAvailableExamsAsync(int userId)
        {
            var student = await _studentRepository.GetByUserId(userId);
            if (student == null) throw new Exception("Student not found");

            var studijskiProgramId = student.StudentStudijskiPrograms.FirstOrDefault()?.StudijskiProgramId;
            if (studijskiProgramId == null) throw new Exception("Study program not found");

            var exams = await _ispitRepository.GetAvailableExamsForProgramAsync(studijskiProgramId.Value);

            return exams.Select(i => new DostupniIspitiDto
            {
                IspitId = i.IspitId,
                PredmetNaziv = i.Predmet.Naziv,
                DatumIspita = i.DatumIspita.Value
            }).ToList();
        }

        public async Task<PrijavaIspitaDto> GetStudentExamRegistrationDataAsync(int userId)
        {
            var student = await _studentRepository.GetByUserId(userId);
            if (student == null) throw new Exception("Student not found");

            return new PrijavaIspitaDto
            {
                TodaysDate = DateTime.Now,
                Ime = student.Ime,
                Prezime = student.Prezime,
                BrojIndexa = student.BrojIndexa.ToString(),
                StudijskiProgramNaziv = student.StudentStudijskiPrograms
                    .FirstOrDefault()?.StudijskiProgram?.Naziv ?? "N/A"
            };
        }

        public async Task RegisterForExamAsync(int userId, int ispitId)
        {
            var student = await _studentRepository.GetByUserId(userId);
            if (student == null) throw new Exception("Student not found");

            var exam = await _ispitRepository.GetByIdAsync(ispitId);
            if (exam == null) throw new Exception("Exam not found");

            var hasPendingRegistration = await _prijavaIspitaRepository.ExistsPendingRegistrationAsync(student.StudentId, ispitId);
            if (hasPendingRegistration)
            {
                throw new Exception("Student je vec prijavio ovaj ispit i ceka se odobrenje.");
            }

            var registration = new StudentIspiti
            {
                StudentId = student.StudentId,
                IspitId = ispitId,
                Status = "Na Cekanju"
            };

            await _prijavaIspitaRepository.AddAsync(registration);
            await _prijavaIspitaRepository.SaveChangesAsync();
        }
    }
}


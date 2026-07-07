using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Exceptions;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.Constants;

namespace StudentIndex.Server.Application.Services
{
    public class PrijavaIspitaService : IPrijavaIspitaService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IIspitRepository _ispitRepository;
        private readonly IPrijavaIspitaRepository _prijavaIspitaRepository;

        public PrijavaIspitaService(
            IStudentRepository studentRepository,
            IIspitRepository ispitRepository,
            IPrijavaIspitaRepository prijavaIspitaRepository)
        {
            _studentRepository = studentRepository;
            _ispitRepository = ispitRepository;
            _prijavaIspitaRepository = prijavaIspitaRepository;
        }

        public async Task<List<DostupniIspitiDto>> GetAvailableExamsAsync(int studentId)
        {
            var student = await _studentRepository.GetByStudentIdAsync(studentId);
            if (student == null) throw new NotFoundException("Student nije pronađen.");

            var studijskiProgramId = student.StudentStudijskiPrograms.FirstOrDefault()?.StudijskiProgramId;
            if (studijskiProgramId == null) throw new NotFoundException("Studijski program nije pronađen.");

            var exams = await _ispitRepository.GetAvailableExamsForProgramAsync(studijskiProgramId.Value);

            return exams.Select(i => new DostupniIspitiDto
            {
                IspitId = i.IspitId,
                PredmetNaziv = i.Predmet?.Naziv ?? string.Empty,
                DatumIspita = i.DatumIspita.GetValueOrDefault()
            }).ToList();
        }

        public async Task<PrijavaIspitaDto> GetStudentExamRegistrationDataAsync(int studentId)
        {
            var student = await _studentRepository.GetByStudentIdAsync(studentId);
            if (student == null) throw new NotFoundException("Student nije pronađen.");

            return new PrijavaIspitaDto
            {
                TodaysDate = DateTime.UtcNow,
                Ime = student.Ime,
                Prezime = student.Prezime,
                BrojIndexa = student.BrojIndexa,
                StudijskiProgramNaziv = student.StudentStudijskiPrograms
                    .FirstOrDefault()?.StudijskiProgram?.Naziv ?? "N/A"
            };
        }

        public async Task RegisterForExamAsync(int studentId, int ispitId)
        {
            var student = await _studentRepository.GetByStudentIdAsync(studentId);
            if (student == null) throw new NotFoundException("Student nije pronađen.");

            var exam = await _ispitRepository.GetByIdAsync(ispitId);
            if (exam == null) throw new NotFoundException("Ispit nije pronađen.");

            // Prijateljska provjera; stvarnu garanciju daje unique index u bazi,
            // a repozitorijum prevodi povredu indeksa u ConflictException.
            if (await _prijavaIspitaRepository.ExistsPendingRegistrationAsync(student.StudentId, ispitId))
                throw new ConflictException("Ispit je već prijavljen i čeka odobrenje.");

            await _prijavaIspitaRepository.AddAsync(new StudentIspiti
            {
                StudentId = student.StudentId,
                IspitId = ispitId,
                Status = StatusIspita.NaCekanju
            });
        }
    }
}

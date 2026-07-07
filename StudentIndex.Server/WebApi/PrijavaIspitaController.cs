using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Extensions;

namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Student)]
    public class PrijavaIspitaController : ControllerBase
    {
        private readonly IPrijavaIspitaService _prijavaIspitaService;

        public PrijavaIspitaController(IPrijavaIspitaService prijavaIspitaService)
        {
            _prijavaIspitaService = prijavaIspitaService;
        }

        [HttpGet("student-data")]
        public async Task<ActionResult<PrijavaIspitaDto>> GetStudentData()
        {
            var studentData = await _prijavaIspitaService
                .GetStudentExamRegistrationDataAsync(User.GetStudentId());
            return Ok(studentData);
        }

        [HttpGet("available-exams")]
        public async Task<ActionResult<List<DostupniIspitiDto>>> GetAvailableExams()
        {
            var availableExams = await _prijavaIspitaService
                .GetAvailableExamsAsync(User.GetStudentId());
            return Ok(availableExams);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterForExam([FromBody] RegisterForExamRequest request)
        {
            await _prijavaIspitaService.RegisterForExamAsync(User.GetStudentId(), request.IspitId);
            return Ok(new { message = "Prijava ispita je uspješna." });
        }
    }
}

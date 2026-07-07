using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Application.Queries;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Extensions;

namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Student)]
    public class PrijavaIspitaController : BaseController
    {
        private readonly IPrijavaIspitaService _prijavaIspitaService;

        public PrijavaIspitaController(IPrijavaIspitaService prijavaIspitaService)
        {
            _prijavaIspitaService = prijavaIspitaService;
        }

        [HttpGet("student-data")]
        public async Task<IActionResult> GetStudentData()
        {
            var studentData = await _prijavaIspitaService
                .GetStudentExamRegistrationDataAsync(User.GetStudentId());
            return SmartResult(studentData);
        }

        [HttpGet("available-exams")]
        public async Task<IActionResult> GetAvailableExams(QueryParameters parameters)
        {
            var availableExams = await _prijavaIspitaService
                .GetAvailableExamsQueryAsync(User.GetStudentId());
            return SmartResult(availableExams, parameters);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterForExam([FromBody] RegisterForExamRequest request)
        {
            await _prijavaIspitaService.RegisterForExamAsync(User.GetStudentId(), request.IspitId);
            return Ok(new { message = "Prijava ispita je uspješna." });
        }
    }
}

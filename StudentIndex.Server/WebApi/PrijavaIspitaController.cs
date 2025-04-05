using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;
using System.Security.Claims;

namespace YourApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PrijavaIspitaController : ControllerBase
    {
        private readonly IPrijavaIspitaService _prijavaIspitaService;

        public PrijavaIspitaController(IPrijavaIspitaService prijavaIspitaService)
        {
            _prijavaIspitaService = prijavaIspitaService;
        }

        [HttpGet("student-data")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<PrijavaIspitaDto>> GetStudentData()
        {
            try
            {
                // Get the logged-in user's ID from the JWT token
                if (!int.TryParse(User.FindFirst("StudentId")?.Value, out int studentIdClaim))
                {
                    return Unauthorized("StudentId not found in token.");
                }

                var studentData = await _prijavaIspitaService.GetStudentExamRegistrationDataAsync(studentIdClaim);
                return Ok(studentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("available-exams")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<List<DostupniIspitiDto>>> GetAvailableExams()
        {
            try
            {
                if (!int.TryParse(User.FindFirst("StudentId")?.Value, out int studentIdClaim))
                {
                    return Unauthorized("StudentId not found in token.");
                }

                var availableExams = await _prijavaIspitaService.GetAvailableExamsAsync(studentIdClaim);
                return Ok(availableExams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> RegisterForExam([FromBody] int ispitId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst("StudentId")?.Value, out int studentIdClaim))
                {
                    return Unauthorized("StudentId not found in token.");
                }

                await _prijavaIspitaService.RegisterForExamAsync(studentIdClaim, ispitId);
                return Ok(new { message = "Exam registration successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;


namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredmetiController : ControllerBase
    {
        private readonly IPredmetService _studentService;

        public PredmetiController(IPredmetService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("moji-predmeti")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<PredmetiDto>>> GetStudentSubjects(
            [FromQuery] int semesterId)
        {

            if (!int.TryParse(User.FindFirst("StudentId")?.Value, out int studentIdClaim))
            {
                return Unauthorized("StudentId not found in token.");
            }

            var subjects = await _studentService.GetStudentPredmetiPoSemestru(studentIdClaim, semesterId);

  

            return Ok(subjects);

        }
    }
}
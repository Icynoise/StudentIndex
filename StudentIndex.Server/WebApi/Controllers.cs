// StudentIndex.Web/Controllers/StudentsController.cs
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{studentId}/moji-predmeti")]
        public async Task<ActionResult<IEnumerable<PredmetiDto>>> GetStudentSubjects(
            int studentId,
            [FromQuery] int yearId,
            [FromQuery] int semesterId)
        {
            var subjects = await _studentService.GetStudentSubjectsAsync(studentId, yearId, semesterId);
            return Ok(subjects);
        }
    }
}
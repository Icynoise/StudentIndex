using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.DTOs;

namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentiController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet("details")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<StudentDto>> GetStudentDetails()
        {
            if (!int.TryParse(User.FindFirst("StudentId")?.Value, out int studentId))
            {
                return Unauthorized("StudentId not found or invalid in token.");
            }

            var student = await _studentService.GetByUserId(studentId);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }
    }
}

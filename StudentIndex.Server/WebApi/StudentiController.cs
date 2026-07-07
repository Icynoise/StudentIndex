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
    public class StudentiController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentiController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("details")]
        public async Task<ActionResult<StudentDto>> GetStudentDetails()
        {
            var student = await _studentService.GetByUserId(User.GetStudentId());
            return Ok(student);
        }
    }
}

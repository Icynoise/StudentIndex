using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Extensions;

namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Student)]
    public class StudentiController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentiController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetStudentDetails()
        {
            var student = await _studentService.GetByUserId(User.GetStudentId());
            return SmartResult(student);
        }
    }
}

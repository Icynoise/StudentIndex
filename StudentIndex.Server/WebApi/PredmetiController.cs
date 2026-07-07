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
    public class PredmetiController : ControllerBase
    {
        private readonly IPredmetService _predmetService;

        public PredmetiController(IPredmetService predmetService)
        {
            _predmetService = predmetService;
        }

        [HttpGet("moji-predmeti")]
        public async Task<ActionResult<IEnumerable<PredmetiDto>>> GetStudentSubjects(
            [FromQuery] int semesterId)
        {
            var subjects = await _predmetService
                .GetStudentPredmetiPoSemestru(User.GetStudentId(), semesterId);
            return Ok(subjects);
        }
    }
}

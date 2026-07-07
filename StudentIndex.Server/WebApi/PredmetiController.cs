using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Application.Queries;
using StudentIndex.Server.Domain.Constants;
using StudentIndex.Server.Extensions;

namespace StudentIndex.Server.WebApi
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Student)]
    public class PredmetiController : BaseController
    {
        private readonly IPredmetService _predmetService;

        public PredmetiController(IPredmetService predmetService)
        {
            _predmetService = predmetService;
        }

        [HttpGet("moji-predmeti")]
        public async Task<IActionResult> GetStudentSubjects(
            [FromQuery] int semesterId, QueryParameters parameters)
        {
            var subjects = await _predmetService
                .GetStudentPredmetiPoSemestru(User.GetStudentId(), semesterId);

            // Lista je već materijalizovana (kombinuje se u memoriji) — SmartResult radi LINQ-to-Objects
            return SmartResult(subjects.AsQueryable(), parameters);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Queries;

namespace StudentIndex.Server.WebApi;

/// <summary>
/// Bazni kontroler sa SmartResult podrškom: pretvara QueryParameters iz query stringa
/// u filtriran, sortiran i paginiran odgovor nad bilo kojim IQueryable izvorom.
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Primjenjuje quick search, filtere po kolonama, sortiranje i paginaciju na izvor.
    /// Bez parametara vraća sve (kompatibilno sa postojećim pozivima);
    /// returnNumberOfRows=true vraća samo broj redova; nevalidne kolone → 400.
    /// </summary>
    protected IActionResult SmartResult<T>(IQueryable<T>? source, QueryParameters? parameters)
    {
        if (source == null)
            return NotFound();

        parameters ??= new QueryParameters();

        var applied = source.ApplyQueryParameters(parameters);
        if (applied.Errors.Count > 0)
            return BadRequest(new { errors = applied.Errors });

        if (parameters.ReturnNumberOfRows)
            return Ok(applied.CountQuery.Count());

        return Ok(applied.DataQuery.ToList());
    }

    /// <summary>Za pojedinačne zapise: null → 404, inače 200.</summary>
    protected IActionResult SmartResult<T>(T? model)
    {
        if (model == null)
            return NotFound();

        return Ok(model);
    }
}

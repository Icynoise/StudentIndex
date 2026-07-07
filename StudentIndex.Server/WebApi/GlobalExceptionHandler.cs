using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentIndex.Server.Application.Exceptions;

namespace StudentIndex.Server.WebApi;

/// <summary>
/// Centralno mapiranje izuzetaka na HTTP odgovore (ProblemDetails) —
/// kontroleri više nemaju try/catch blokove.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, exception.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Došlo je do neočekivane greške.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Neobrađena greška na {Path}", httpContext.Request.Path);
        else
            _logger.LogWarning("{ExceptionType} na {Path}: {Message}",
                exception.GetType().Name, httpContext.Request.Path, exception.Message);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = title
        }, cancellationToken);

        return true;
    }
}

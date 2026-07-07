using StudentIndex.Server.Application.Exceptions;
using StudentIndex.Server.Domain.Constants;
using System.Security.Claims;

namespace StudentIndex.Server.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Vadi StudentId iz JWT claim-ova. Baca UnauthorizedException (→ 401)
    /// ako claim nedostaje ili nije validan broj.
    /// </summary>
    public static int GetStudentId(this ClaimsPrincipal user)
    {
        if (!int.TryParse(user.FindFirst(CustomClaimTypes.StudentId)?.Value, out var studentId))
            throw new UnauthorizedException("StudentId nije pronađen u tokenu.");

        return studentId;
    }
}

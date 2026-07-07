using System.ComponentModel.DataAnnotations;

namespace StudentIndex.Server.Application.DTOs;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}

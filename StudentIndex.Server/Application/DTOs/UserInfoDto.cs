namespace StudentIndex.Server.Application.DTOs;

/// <summary>
/// Podaci o Identity korisniku koje Application sloj koristi bez direktne
/// zavisnosti od ASP.NET Identity tipova.
/// </summary>
public class UserInfoDto
{
    public string UserId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? StudentId { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}

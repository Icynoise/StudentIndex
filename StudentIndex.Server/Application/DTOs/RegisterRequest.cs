using System.ComponentModel.DataAnnotations;

namespace StudentIndex.Server.Application.DTOs;

public class RegisterStudentRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Ime { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Prezime { get; set; } = null!;

    [Required]
    [StringLength(15)]
    public string BrojIndexa { get; set; } = null!;

    [EmailAddress]
    public string? EmailStudent { get; set; }

    [Phone]
    public string? Telefon { get; set; }

    public DateOnly DatumRodjenja { get; set; }

    public string? Status { get; set; }
}

using StudentIndex.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentIndex.Server.Core;


public partial class Studenti
{
    public int StudentId { get; set; }

    [Required]
    [StringLength(50)]
    public string Ime { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Prezime { get; set; } = null!;

    public DateOnly DatumRođenja { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? Telefon { get; set; }

    public string? Status { get; set; } 

    public virtual ICollection<StudentIspiti> StudentIspitis { get; set; } = new List<StudentIspiti>();

    public virtual ICollection<StudentStudijskiProgram> StudentStudijskiPrograms { get; set; } = new List<StudentStudijskiProgram>();

}

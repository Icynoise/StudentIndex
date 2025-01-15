using System;
using System.Collections.Generic;

namespace StudentIndex.Models;

public partial class Studenti
{
    public int StudentId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public DateOnly DatumRođenja { get; set; }

    public string? Email { get; set; }

    public string? Telefon { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<StudentIspiti> StudentIspitis { get; set; } = new List<StudentIspiti>();

    public virtual ICollection<StudentStudijskiProgram> StudentStudijskiPrograms { get; set; } = new List<StudentStudijskiProgram>();
}

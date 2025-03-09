
using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Domain;

public partial class StudijskiProgrami
{
    public int StudijskiProgramId { get; set; }

    public int? FakultetId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public virtual OsnovneStudije? Fakultet { get; set; }

    public virtual ICollection<PredmetiUprogramima> PredmetiUprogramimas { get; set; } = new List<PredmetiUprogramima>();

    public virtual ICollection<StudentStudijskiProgram> StudentStudijskiPrograms { get; set; } = new List<StudentStudijskiProgram>();
}

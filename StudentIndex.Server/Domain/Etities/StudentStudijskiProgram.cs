using StudentIndex.Server.Domain;
using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Domain;

public partial class StudentStudijskiProgram
{
    public int StudentStudijskiProgramId { get; set; }

    public int? StudentId { get; set; }

    public int? StudijskiProgramId { get; set; }

    public DateOnly? DatumPočetka { get; set; }

    public DateOnly? DatumZavršetka { get; set; }

    public virtual Studenti? Student { get; set; }

    public virtual StudijskiProgrami? StudijskiProgram { get; set; }
}

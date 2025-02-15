using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Core;

public partial class OsnovneStudije
{
    public int FakultetId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public virtual ICollection<StudijskiProgrami> StudijskiProgramis { get; set; } = new List<StudijskiProgrami>();
}

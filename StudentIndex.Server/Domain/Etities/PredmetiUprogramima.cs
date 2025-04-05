using StudentIndex.Server.Domain.Etities;
using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Domain;

public partial class PredmetiUprogramima
{
    public int PredmetUprogramuId { get; set; }

    public int StudijskiProgramId { get; set; }

    public int PredmetId { get; set; }

    public int GodinaId { get; set; }

    public int SemestarId { get; set; }

    public virtual Godine Godina { get; set; } = null!;

    public virtual Predmeti Predmet { get; set; } = null!;

    public virtual StudijskiProgrami StudijskiProgram { get; set; } = null!;

    public virtual Semestri Semestar { get; set; } = null!;
}

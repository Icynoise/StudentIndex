using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Core;

public partial class Ispiti
{
    public int IspitId { get; set; }

    public int? PredmetId { get; set; }

    public DateOnly? DatumIspita { get; set; }

    public string? TipIspita { get; set; }

    public virtual Predmeti? Predmet { get; set; }

    public virtual ICollection<StudentIspiti> StudentIspitis { get; set; } = new List<StudentIspiti>();
}

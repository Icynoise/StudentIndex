using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Domain;

public partial class Ispiti
{
    public int IspitId { get; set; }

    public int? PredmetId { get; set; }

    public DateTime DatumIspita { get; set; }

    public DateTime? RokZaRegistraciju { get; set; }

    public string Status { get; set; } = "Open";

    public int Rokovi { get; set; }

    public virtual Predmeti? Predmet { get; set; }

    public virtual ICollection<StudentIspiti> StudentIspitis { get; set; } = new List<StudentIspiti>();

}

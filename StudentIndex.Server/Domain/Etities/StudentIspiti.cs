using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Domain;

public partial class StudentIspiti
{
    public int StudentIspitId { get; set; }

    public int? StudentId { get; set; }

    public int? IspitId { get; set; }

    public string? RezultatIspita { get; set; }

    public string Status { get; set; } = "Na Cekanju";

    public virtual Ispiti? Ispit { get; set; }

    public virtual ICollection<PokušajiIspitum> PokušajiIspita { get; set; } = new List<PokušajiIspitum>();

    public virtual Studenti? Student { get; set; }
}

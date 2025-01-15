using System;
using System.Collections.Generic;

namespace StudentIndex.Models;

public partial class PokušajiIspitum
{
    public int PokušajIspitaId { get; set; }

    public int? StudentIspitId { get; set; }

    public DateOnly? DatumPokušaja { get; set; }

    public string? RezultatIspita { get; set; }

    public virtual StudentIspiti? StudentIspit { get; set; }
}

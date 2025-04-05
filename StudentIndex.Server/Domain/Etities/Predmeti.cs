using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentIndex.Server.Domain;

public partial class Predmeti
{
    public int PredmetId { get; set; }

    public string Naziv { get; set; } = null!;

    public string Opis { get; set; } = null!;

    public short Ects { get; set; }

    [NotMapped]
    public string? RezultatIspita { get; set; }


    // Example behavior
    public string GetStatus()
    {
        if (string.IsNullOrEmpty(RezultatIspita))
            return "Nema izlazaka na ispit";

        if (int.TryParse(RezultatIspita, out int rezultat))
            return rezultat > 5 ? "Polozeno" : "Nepolozeno";

        return "Nevalidan unos";
    }


    public virtual ICollection<Ispiti> Ispitis { get; set; } = new List<Ispiti>();

    public virtual ICollection<PredmetiUprogramima> PredmetiUprogramimas { get; set; } = new List<PredmetiUprogramima>();
}

using System;
using System.Collections.Generic;

namespace StudentIndex.Server.Core;

public partial class Godine
{
    public int GodinaId { get; set; }

    public string NazivGodine { get; set; } = null!;

    public virtual ICollection<PredmetiUprogramima> PredmetiUprogramimas { get; set; } = new List<PredmetiUprogramima>();
}

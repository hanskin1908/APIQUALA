using System;
using System.Collections.Generic;

namespace APIQUALA.Models;

public partial class MonMonedum
{
    public int IdMoneda { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Sucursal> Sucursals { get; } = new List<Sucursal>();
}

using System;
using System.Collections.Generic;

namespace APIQUALA.Models;

public partial class Sucursal
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public int? IdMonedaSuc { get; set; }

    public virtual MonMonedum? IdMonedaSucNavigation { get; set; }
}

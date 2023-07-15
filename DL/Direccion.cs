using System;
using System.Collections.Generic;

namespace DL;

public partial class Direccion
{
    public int IdDireccion { get; set; }

    public string? Calle { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public int IdColonia { get; set; }

    public int IdUsuario { get; set; }

    public virtual Colonium IdColoniaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

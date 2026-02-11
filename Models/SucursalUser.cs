using System;

namespace ComprasVentas.Models;

public class SucursalUser
{
    public int Id { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public Sucursal? Sucursal { get; set; }

    public Usuario? Usuario { get; set; }


}
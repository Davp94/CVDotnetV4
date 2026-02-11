using System;

namespace ComprasVentas.Models;

public class Sucursal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public ICollection<SucursalUser> SucursalUsers { get; set; } = [];

    public ICollection<Almacen> Almacenes { get; set; } = [];
}
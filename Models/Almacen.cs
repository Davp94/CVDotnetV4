using System;

namespace ComprasVentas.Models;

public class Almacen
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public string Direccion { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;

    public Sucursal? Sucursal { get; set; }

    public ICollection<AlmacenProducto> AlmacenProductos { get; set; } = [];

    public ICollection<Movimiento> Movimientos { get; set; } = [];
}
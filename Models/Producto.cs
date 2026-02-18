using System;

namespace ComprasVentas.Models;

public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string? UnidadMedida { get; set; }

    public string? Marca { get; set; }

    public decimal PrecioVentaActual { get; set; }

    public string? Imagen { get; set; }

    public bool Estado { get; set; }

    public Categoria? Categoria { get; set; }

    public ICollection<AlmacenProducto> AlmacenProductos { get; set; } = [];

    public ICollection<Movimiento> Movimientos { get; set; } = [];
}
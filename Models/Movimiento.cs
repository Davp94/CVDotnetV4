using System;

namespace ComprasVentas.Models;

public class Movimiento
{
    public int Id { get; set; }
    public decimal Cantidad { get; set; }
    public string TipoMovimiento { get; set; }
    public decimal PrecioUnitarioCompra { get; set; }
    public decimal PrecioUnitarioVenta { get; set; }
    public string Observaciones { get; set; }

    public Producto Producto { get; set; }

    public Almacen Almacen { get; set; }

    public Nota Nota { get; set; }
}

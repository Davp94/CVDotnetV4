using System;

namespace ComprasVentas.Models;

public class AlmacenProducto
{
    public int Id { get; set; }

    public int CantidadActual { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public Producto? Producto { get; set; }

    public Almacen? Almacen { get; set; }
}
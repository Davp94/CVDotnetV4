using System;

namespace ComprasVentas.Dto;

public class CreateMovimientoDto
{
    public decimal Cantidad { get; set; }
    public string TipoMovimiento { get; set; }
    public decimal PrecioUnitarioCompra { get; set; }
    public decimal PrecioUnitarioVenta { get; set; }
    public string Observaciones { get; set; }

    public int ProductoId { get; set; }

    public int AlmacenId { get; set; }

    public int NotaId { get; set; }
}

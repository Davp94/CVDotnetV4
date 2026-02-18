using System;

namespace ComprasVentas.Dto;

public class NotaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string TipoNota { get; set; }
    public decimal Impuestos { get; set; }

    public decimal Total { get; set; }

    public decimal Descuentos { get; set; }

    public bool Estado { get; set; }

    public string Observaciones { get; set; }
}

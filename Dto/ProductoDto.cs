using System;

namespace ComprasVentas.Dto;

public class ProductoDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string? UnidadMedida { get; set; }

    public string? Marca { get; set; }

    public decimal Precio { get; set; }

    public string? Imagen { get; set; }

    public bool Estado { get; set; }

    public int CategoriaId { get; set; }
}

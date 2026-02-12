using System;

namespace ComprasVentas.Dto;

public class CreateProductoDto
{
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public IFormFile? Imagen { get; set; } //alternativa base64

    public bool Estado { get; set; }

    public int CategoriaId { get; set; }
}

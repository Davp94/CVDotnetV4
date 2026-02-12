using System;

namespace ComprasVentas.Dto;

public class AlmacenDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public string Direccion { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;
}
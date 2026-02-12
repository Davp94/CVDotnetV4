using System;

namespace ComprasVentas.Dto;

public class SucursalDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Ciudad { get; set; } = string.Empty;
}
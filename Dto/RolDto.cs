using System;

namespace ComprasVentas.Dto;

public class RolDto
{

    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public List<int> PermisosIds { get; set; } = [];
}

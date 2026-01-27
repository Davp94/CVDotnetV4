using System;

namespace ComprasVentas.Models;

public class Permiso
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Recurso { get; set; }

    public string Accion { get; set; } = string.Empty;

    // Relacion many to many
    public ICollection<Rol> Roles { get; set; } = new List<Rol>();
}

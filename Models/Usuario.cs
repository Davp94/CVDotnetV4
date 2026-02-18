using System;

namespace ComprasVentas.Models;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Persona? Persona { get; set; }

    public ICollection<Rol> Roles { get; set; } = [];

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    public ICollection<SucursalUser> SucursalUsers { get; set; } = [];

    public ICollection<Nota> Notas { get; set; } = [];
}

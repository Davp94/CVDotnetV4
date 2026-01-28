using System;

namespace ComprasVentas.Models;

public class Persona
{
    public int Id { get; set; }

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public DateTime FechaNacimiento { get; set; }

    public string? Genero { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Nacionalidad { get; set; }

    public Usuario? Usuario { get; set; }

}

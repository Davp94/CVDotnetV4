using System;

namespace ComprasVentas.Models;

public class ClienteProveedor
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public string RazonSocial { get; set; }
    public string NroIdentificacion { get; set; }
    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public string Correo { get; set; }

    public bool Estado { get; set;}

    public ICollection<Nota> Notas { get; set; }
}

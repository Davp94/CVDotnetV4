using System;
using ComprasVentas.Models;

namespace ComprasVentas.Builder;

public class RolBuilder
{
    private readonly Rol _rol = new Rol();

    public RolBuilder WithNombre(string nombre)
    {
        _rol.Nombre = nombre;
        return this;
    }

    public RolBuilder WithDescripcion(string descripcion)
    {
        _rol.Descripcion = descripcion;
        return this;
    }

    public RolBuilder WithPermisos(List<Permiso> permisos)
    {
        _rol.Permisos = permisos;
        return this;
    }

    public Rol Build()
    {
        return _rol;
    }
}

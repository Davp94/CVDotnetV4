using ComprasVentas.Data;
using ComprasVentas.Models;

namespace ComprasVentas.Seeders.Dev;

public class PermisoSeeder
{
    private readonly AppDbContext _context;

    public PermisoSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Permisos.Any())
            return;

        var recursos = new[]
        {
            "usuarios", "roles", "permisos", "productos", "categorias",
            "sucursales", "almacenes", "cliente-proveedores", "notas", "movimientos"
        };

        var acciones = new[] { "crear", "leer", "actualizar", "eliminar" };

        var permisos = new List<Permiso>();

        foreach (var recurso in recursos)
        {
            foreach (var accion in acciones)
            {
                permisos.Add(new Permiso
                {
                    Nombre = $"{accion}:{recurso}",
                    Recurso = recurso,
                    Accion = accion
                });
            }
        }

        await _context.Permisos.AddRangeAsync(permisos);
        await _context.SaveChangesAsync();
    }
}

using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Seeders.Dev;

public class RolSeeder
{
    private readonly AppDbContext _context;

    public RolSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Roles.Any())
            return;

        var todosLosPermisos = await _context.Permisos.ToListAsync();

        var admin = new Rol
        {
            Nombre = "ADMIN",
            Descripcion = "Administrador del sistema con acceso completo",
            Permisos = todosLosPermisos
        };

        var vendedorPermisos = todosLosPermisos
            .Where(p => p.Recurso != null && new[] { "productos", "categorias", "notas", "movimientos", "cliente-proveedores" }.Contains(p.Recurso))
            .ToList();

        var vendedor = new Rol
        {
            Nombre = "VENDEDOR",
            Descripcion = "Vendedor con acceso a productos y notas de venta",
            Permisos = vendedorPermisos
        };

        var cajeroPermisos = todosLosPermisos
            .Where(p => p.Recurso != null && new[] { "notas", "cliente-proveedores", "productos" }.Contains(p.Recurso)
                        && new[] { "leer", "crear" }.Contains(p.Accion))
            .ToList();

        var cajero = new Rol
        {
            Nombre = "CAJERO",
            Descripcion = "Cajero con acceso limitado a notas y consulta de productos",
            Permisos = cajeroPermisos
        };

        await _context.Roles.AddRangeAsync(admin, vendedor, cajero);
        await _context.SaveChangesAsync();
    }
}

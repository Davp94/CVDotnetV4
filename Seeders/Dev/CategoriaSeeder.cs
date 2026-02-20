using System;
using ComprasVentas.Data;
using ComprasVentas.Models;

namespace ComprasVentas.Seeders.Dev;

public class CategoriaSeeder
{
    private readonly AppDbContext _context;

    public CategoriaSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if(_context.Categorias.Any())
            return;

        var categorias = new List<Categoria>
        {
            new Categoria { Nombre = "Electrónica" },
            new Categoria { Nombre = "Ropa" },
            new Categoria { Nombre = "Hogar" },
            new Categoria { Nombre = "Deportes" },
            new Categoria { Nombre = "Libros" }
        };

        foreach (var categoria in categorias)
        {
            _context.Categorias.Add(categoria);
        }

        await _context.SaveChangesAsync();
    }
}

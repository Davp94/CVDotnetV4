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
        if (_context.Categorias.Any())
            return;

        var categorias = new List<Categoria>
        {
            new() { Nombre = "Electrónica" },
            new() { Nombre = "Ropa y Moda" },
            new() { Nombre = "Hogar y Jardín" },
            new() { Nombre = "Deportes y Aire Libre" },
            new() { Nombre = "Libros y Papelería" },
            new() { Nombre = "Alimentos y Bebidas" },
            new() { Nombre = "Herramientas" },
            new() { Nombre = "Juguetes" },
            new() { Nombre = "Salud y Belleza" },
            new() { Nombre = "Automotriz" }
        };

        await _context.Categorias.AddRangeAsync(categorias);
        await _context.SaveChangesAsync();
    }
}

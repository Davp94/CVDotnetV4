using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Seeders.Dev;

public class ProductoSeeder
{
    private readonly AppDbContext _context;

    public ProductoSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Productos.Any())
            return;

        var categorias = await _context.Categorias.ToListAsync();

        if (!categorias.Any())
            return;

        var unidades = new[] { "unidad", "kg", "litro", "metro", "caja", "par", "docena" };

        var faker = new Faker<Producto>("es")
            .RuleFor(p => p.Nombre,            f => f.Commerce.ProductName())
            .RuleFor(p => p.Descripcion,       f => f.Commerce.ProductDescription())
            .RuleFor(p => p.UnidadMedida,      f => f.PickRandom(unidades))
            .RuleFor(p => p.Marca,             f => f.Company.CompanyName())
            .RuleFor(p => p.PrecioVentaActual, f => f.Random.Decimal(5, 2000))
            .RuleFor(p => p.Imagen,            f => f.Image.PicsumUrl(200, 200))
            .RuleFor(p => p.Estado,            f => f.Random.Bool(0.9f))
            .RuleFor(p => p.Categoria,         f => f.PickRandom(categorias));

        // Insert in batches to avoid memory pressure
        const int total     = 1000;
        const int batchSize = 250;

        for (int i = 0; i < total; i += batchSize)
        {
            var batch = faker.Generate(batchSize);
            await _context.Productos.AddRangeAsync(batch);
            await _context.SaveChangesAsync();
        }
    }
}

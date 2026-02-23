using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Seeders.Dev;

public class AlmacenProductoSeeder
{
    private readonly AppDbContext _context;

    public AlmacenProductoSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.AlmacenProductos.Any())
            return;

        var almacenes = await _context.Almacenes.ToListAsync();
        var productos = await _context.Productos.ToListAsync();

        if (!almacenes.Any() || !productos.Any())
            return;

        var faker = new Faker("es");
        var relaciones = new List<AlmacenProducto>();

        // Assign every producto to one random almacen
        foreach (var producto in productos)
        {
            var almacen = faker.PickRandom(almacenes);
            relaciones.Add(new AlmacenProducto
            {
                Producto          = producto,
                Almacen           = almacen,
                CantidadActual    = faker.Random.Int(0, 500),
                FechaActualizacion = DateTime.UtcNow
            });
        }

        // Bulk insert in batches to avoid massive single transaction
        const int batchSize = 500;
        for (int i = 0; i < relaciones.Count; i += batchSize)
        {
            var batch = relaciones.Skip(i).Take(batchSize).ToList();
            await _context.AlmacenProductos.AddRangeAsync(batch);
            await _context.SaveChangesAsync();
        }
    }
}

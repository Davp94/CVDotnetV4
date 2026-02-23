using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Seeders.Dev;

public class AlmacenSeeder
{
    private readonly AppDbContext _context;

    public AlmacenSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Almacenes.Any())
            return;

        var sucursales = await _context.Sucursales.ToListAsync();

        var almacenes = new List<Almacen>();
        int codigoCounter = 1;

        foreach (var sucursal in sucursales)
        {
            var faker = new Faker("es");
            for (int i = 1; i <= 4; i++)
            {
                almacenes.Add(new Almacen
                {
                    Nombre      = $"Almacén {i} - {sucursal.Nombre}",
                    Codigo      = $"ALM-{codigoCounter:D4}",
                    Descripcion = faker.Lorem.Sentence(),
                    Direccion   = faker.Address.StreetAddress(),
                    Telefono    = faker.Phone.PhoneNumber("####-####"),
                    Ciudad      = sucursal.Ciudad,
                    Sucursal    = sucursal
                });
                codigoCounter++;
            }
        }

        await _context.Almacenes.AddRangeAsync(almacenes);
        await _context.SaveChangesAsync();
    }
}

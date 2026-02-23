using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;

namespace ComprasVentas.Seeders.Dev;

public class SucursalSeeder
{
    private readonly AppDbContext _context;

    public SucursalSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Sucursales.Any())
            return;

        var faker = new Faker<Sucursal>("es")
            .RuleFor(s => s.Nombre,    f => $"Sucursal {f.Address.City()}")
            .RuleFor(s => s.Direccion, f => f.Address.StreetAddress())
            .RuleFor(s => s.Telefono,  f => f.Phone.PhoneNumber("####-####"))
            .RuleFor(s => s.Ciudad,    f => f.Address.City());

        var sucursales = faker.Generate(10);
        await _context.Sucursales.AddRangeAsync(sucursales);
        await _context.SaveChangesAsync();
    }
}

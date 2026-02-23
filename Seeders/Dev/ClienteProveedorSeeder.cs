using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;

namespace ComprasVentas.Seeders.Dev;

public class ClienteProveedorSeeder
{
    private readonly AppDbContext _context;

    public ClienteProveedorSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.ClienteProveedor.Any())
            return;

        var tipos = new[] { "CLIENTE", "PROVEEDOR", "AMBOS" };

        var faker = new Faker<ClienteProveedor>("es")
            .RuleFor(cp => cp.Tipo,               f => f.PickRandom(tipos))
            .RuleFor(cp => cp.RazonSocial,         f => f.Company.CompanyName())
            .RuleFor(cp => cp.NroIdentificacion,   f => f.Random.Replace("###-######-#"))
            .RuleFor(cp => cp.Telefono,            f => f.Phone.PhoneNumber("####-####"))
            .RuleFor(cp => cp.Direccion,           f => f.Address.StreetAddress())
            .RuleFor(cp => cp.Correo,              f => f.Internet.Email())
            .RuleFor(cp => cp.Estado,              f => f.Random.Bool(0.9f));

        var clienteProveedores = faker.Generate(50);
        await _context.ClienteProveedor.AddRangeAsync(clienteProveedores);
        await _context.SaveChangesAsync();
    }
}

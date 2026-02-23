using Bogus;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Seeders.Dev;

public class UsuarioSeeder
{
    private readonly AppDbContext _context;

    public UsuarioSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Usuarios.Any())
            return;

        var roles = await _context.Roles.ToListAsync();
        var rolAdmin    = roles.First(r => r.Nombre == "ADMIN");
        var rolVendedor = roles.First(r => r.Nombre == "VENDEDOR");
        var rolCajero   = roles.First(r => r.Nombre == "CAJERO");

        // Assign roles by index: 0 → ADMIN, 1-4 → VENDEDOR, 5-9 → CAJERO
        Rol PickRol(int index) => index switch
        {
            0 => rolAdmin,
            < 5 => rolVendedor,
            _ => rolCajero
        };

        var fakerPersona = new Faker<Persona>("es")
            .RuleFor(p => p.Nombres,         f => f.Name.FirstName())
            .RuleFor(p => p.Apellidos,       f => f.Name.LastName())
            .RuleFor(p => p.FechaNacimiento, f => f.Date.Past(40, DateTime.UtcNow.AddYears(-18)).ToUniversalTime())
            .RuleFor(p => p.Genero,          f => f.PickRandom("Masculino", "Femenino"))
            .RuleFor(p => p.Telefono,        f => f.Phone.PhoneNumber("####-####"))
            .RuleFor(p => p.Direccion,       f => f.Address.StreetAddress())
            .RuleFor(p => p.Nacionalidad,    f => f.Address.Country());

        var usuarios = new List<Usuario>();

        for (int i = 0; i < 10; i++)
        {
            var persona = fakerPersona.Generate();
            var index   = i;

            var faker = new Faker("es");
            var nombre  = faker.Internet.UserName(persona.Nombres, persona.Apellidos);
            var correo  = faker.Internet.Email(persona.Nombres, persona.Apellidos);

            var usuario = new Usuario
            {
                Nombre   = nombre,
                Correo   = correo,
                Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                Persona  = persona,
                Roles    = new List<Rol> { PickRol(index) }
            };

            usuarios.Add(usuario);
        }

        await _context.Usuarios.AddRangeAsync(usuarios);
        await _context.SaveChangesAsync();
    }
}

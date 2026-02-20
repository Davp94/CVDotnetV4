using System;
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
        if(_context.Productos.Any())
            return;

        var categorias = await _context.Categorias.ToListAsync();

        var faker = new Faker<Producto>("es")
            .RuleFor(p => p.Nombre, f => f.Finance.AccountName())
            .RuleFor(p => p.Descripcion, f => f.Lorem.Sentence())
            .RuleFor(p => p.Categoria, f => f.PickRandom(categorias))
            .RuleFor(p => p.PrecioVentaActual, f => f.Random.Decimal(10, 1000))
            .RuleFor(p => p.Marca, f => f.Internet.DomainWord())
            .RuleFor(p => p.Imagen, f => f.Image.PicsumUrl(200, 200));

        var productos = faker.Generate(10000); 
        await _context.Productos.AddRangeAsync(productos);
        await _context.SaveChangesAsync();   
    }
}

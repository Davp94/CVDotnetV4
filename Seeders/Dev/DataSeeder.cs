using System;

namespace ComprasVentas.Seeders.Dev;

public class DataSeeder
{
    private readonly CategoriaSeeder _categoriaSeeder;
    private readonly ProductoSeeder _productoSeeder;

    public DataSeeder(CategoriaSeeder categoriaSeeder, ProductoSeeder productoSeeder)
    {
        _categoriaSeeder = categoriaSeeder;
        _productoSeeder = productoSeeder;
    }

    public async Task SeedAsync()
    {
        await _categoriaSeeder.SeedAsync();
        await _productoSeeder.SeedAsync();
    }
}

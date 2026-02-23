using System;

namespace ComprasVentas.Seeders.Dev;

public class DataSeeder
{
    private readonly PermisoSeeder _permisoSeeder;
    private readonly RolSeeder _rolSeeder;
    private readonly UsuarioSeeder _usuarioSeeder;
    private readonly CategoriaSeeder _categoriaSeeder;
    private readonly ProductoSeeder _productoSeeder;
    private readonly SucursalSeeder _sucursalSeeder;
    private readonly AlmacenSeeder _almacenSeeder;
    private readonly AlmacenProductoSeeder _almacenProductoSeeder;
    private readonly ClienteProveedorSeeder _clienteProveedorSeeder;

    public DataSeeder(
        PermisoSeeder permisoSeeder,
        RolSeeder rolSeeder,
        UsuarioSeeder usuarioSeeder,
        CategoriaSeeder categoriaSeeder,
        ProductoSeeder productoSeeder,
        SucursalSeeder sucursalSeeder,
        AlmacenSeeder almacenSeeder,
        AlmacenProductoSeeder almacenProductoSeeder,
        ClienteProveedorSeeder clienteProveedorSeeder)
    {
        _permisoSeeder = permisoSeeder;
        _rolSeeder = rolSeeder;
        _usuarioSeeder = usuarioSeeder;
        _categoriaSeeder = categoriaSeeder;
        _productoSeeder = productoSeeder;
        _sucursalSeeder = sucursalSeeder;
        _almacenSeeder = almacenSeeder;
        _almacenProductoSeeder = almacenProductoSeeder;
        _clienteProveedorSeeder = clienteProveedorSeeder;
    }

    public async Task SeedAsync()
    {
        await _permisoSeeder.SeedAsync();
        await _rolSeeder.SeedAsync();
        await _usuarioSeeder.SeedAsync();
        await _categoriaSeeder.SeedAsync();
        await _productoSeeder.SeedAsync();
        await _sucursalSeeder.SeedAsync();
        await _almacenSeeder.SeedAsync();
        await _almacenProductoSeeder.SeedAsync();
        await _clienteProveedorSeeder.SeedAsync();
    }
}

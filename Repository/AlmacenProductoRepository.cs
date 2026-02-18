using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class AlmacenProductoRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<AlmacenProducto?> GetByAlmacenAndProductoAsync(int almacenId, int productoId)
    {
        return await _context.AlmacenProductos
            .Include(ap => ap.Producto)
            .Include(ap => ap.Almacen)
            .FirstOrDefaultAsync(ap => ap.Almacen != null && ap.Almacen.Id == almacenId && ap.Producto != null && ap.Producto.Id == productoId);
    }

    public async Task UpdateAsync(AlmacenProducto almacenProducto)
    {
        _context.AlmacenProductos.Update(almacenProducto);
        await _context.SaveChangesAsync();
    }
}

using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class SucursalRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Sucursal>> GetAllAsync()
    {
        return await _context.Sucursales.ToListAsync();
    }

    public async Task<List<Almacen>> GetAlmacenesBySucursalIdAsync(int sucursalId)
    {
        return await _context.Almacenes.Where(a => a.Sucursal.Id == sucursalId).ToListAsync();
    }
}
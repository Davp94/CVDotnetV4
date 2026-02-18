using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class MovimientoRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Movimiento>> GetAllAsync()
    {
        return await _context.Movimientos.ToListAsync();
    }

    public async Task<Movimiento?> CreateMovimiento(Movimiento movimiento)
    {
        await _context.Movimientos.AddAsync(movimiento);
        await _context.SaveChangesAsync();
        return movimiento;
    }
}

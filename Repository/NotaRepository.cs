using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class NotaRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Nota>> GetAllAsync()
    {
        return await _context.Notas.ToListAsync();
    }

    public async Task<Nota?> CreateNota(Nota nota)
    {
        await _context.Notas.AddAsync(nota);
        await _context.SaveChangesAsync();
        return nota;
    }

    public async Task<Nota?> GetByIdAsync(int id)
    {
        return await _context.Notas
            .Include(n=>n.ClienteProveedor)
            .Include(n=>n.Movimientos)
            .ThenInclude(m=>m.Producto)
            .FirstOrDefaultAsync(n=>n.Id == id);
    }

}

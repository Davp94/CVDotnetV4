using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class PermisoRepository
{
    private readonly AppDbContext _context;

    public PermisoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Permiso>> GetAllAsync()
    {
        return await _context.Permisos.ToListAsync();
    }

    public async Task<Permiso?> GetByIdAsync(int permisoId)
    {
        return await _context.Permisos.FirstOrDefaultAsync(p => p.Id == permisoId);
    }


    public async Task CreateAsync(Permiso permiso)
    {
        _context.Permisos.Add(permiso);
        await _context.SaveChangesAsync();
    }

}

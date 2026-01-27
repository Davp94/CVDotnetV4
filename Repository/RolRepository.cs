using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class RolRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<Rol>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Rol?> GetByIdAsync(int id)
    {
        return await _context.Roles.Include(r=>r.Permisos).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateAsync(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rol rol)
    {
        _context.Roles.Update(rol);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var rol = await GetByIdAsync(id);
        if (rol != null)
        {
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }
 
    }




}

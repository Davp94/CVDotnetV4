using System;
using ComprasVentas.Data;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Repository;

public class ClienteProveedorRepository(AppDbContext context)
{

    private readonly AppDbContext _context = context;

    public async Task<List<ClienteProveedor>> GetAllAsync()
    {
        return await _context.ClienteProveedor.ToListAsync();
    }

    public async Task<ClienteProveedor> GetByIdAsync(int id)
    {
        return await _context.ClienteProveedor.FirstOrDefaultAsync(cp => cp.Id == id);
    }


}

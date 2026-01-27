using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class PermisoService(PermisoRepository permisoRepository) : IPermisoService    
{
    private readonly PermisoRepository _permisoRepository = permisoRepository;

    public async Task<List<PermisoDto>> GetAllAsync()
    {
        var permisos = await _permisoRepository.GetAllAsync();
        return permisos.Select(p => new PermisoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Recurso = p.Recurso,
            Accion = p.Accion
        }).ToList();
    }

    public async Task<PermisoDto?> GetByIdAsync(int id)
    {
        var permiso = await _permisoRepository.GetByIdAsync(id);
        if (permiso == null) return null;

        return new PermisoDto
        {
            Id = permiso.Id,
            Nombre = permiso.Nombre,
            Recurso = permiso.Recurso,
            Accion = permiso.Accion
        };
    }

    public async Task<PermisoDto> CreateAsync(CreatePermisoDto dto)
    {
        var permiso  = new Permiso
        {
            Nombre = dto.Nombre,
            Recurso = dto.Recurso,
            Accion = dto.Accion
        };

        await _permisoRepository.CreateAsync(permiso);

        return new PermisoDto
        {
            Id = permiso.Id,
            Nombre = permiso.Nombre,
            Recurso = permiso.Recurso,
            Accion = permiso.Accion
        };
    }


}

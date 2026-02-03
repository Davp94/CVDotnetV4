using System;
using ComprasVentas.Builder;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class RolService(RolRepository rolRepository, PermisoRepository permisoRepository) : IRolService
{
    private readonly RolRepository _rolRepository = rolRepository;

    private readonly PermisoRepository _permisoRepository = permisoRepository;


    public async Task<List<RolDto>> GetAllAsync()
    {
        var roles = await _rolRepository.GetAllAsync();
        return [.. roles.Select(r => new RolDto
        {
            Id = r.Id,
            Nombre = r.Nombre,
            Descripcion = r.Descripcion,
            PermisosIds = r.Permisos?.Select(p => p.Id).ToList() ?? []
        })];
    }

    public async Task<RolDto?> GetByIdAsync(int id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);
        if (rol == null) return null;
        return new RolDto
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
            Descripcion = rol.Descripcion,
            PermisosIds = rol.Permisos?.Select(p => p.Id).ToList() ?? []
        };
    }

    public async Task<RolDto> CreateAsync(CreateRolDto dto)
    {
        try
        {
            var permisos = new List<Permiso>();
        foreach (var permisoId in dto.PermisoIds)
        {
            var permiso = await _permisoRepository.GetByIdAsync(permisoId);
            if(permiso != null) permisos.Add(permiso);
        }

        var rol = new RolBuilder()
            .WithNombre(dto.Nombre)
            .WithDescripcion(dto.Descripcion)
            .WithPermisos(permisos)
            .Build();
        
        await _rolRepository.CreateAsync(rol);

        return new RolDto
        {
            Id = rol.Id,
            Nombre = rol.Nombre,
            Descripcion = rol.Descripcion,
            PermisosIds = rol.Permisos?.Select(p => p.Id).ToList() ?? []
        }; 
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task UpdateAsync(int id, CreateRolDto dto)
    {
        var rol = await _rolRepository.GetByIdAsync(id);
        if (rol == null) return;
        rol.Nombre = dto.Nombre;
        rol.Descripcion = dto.Descripcion;
        rol.Permisos.Clear();

        foreach (var permisoId in dto.PermisoIds)
        {
            var permiso = await _permisoRepository.GetByIdAsync(permisoId);
            if(permiso != null) rol.Permisos.Add(permiso);
        }

        await _rolRepository.UpdateAsync(rol);
    }

    public async Task DeleteAsync(int id)
    {
        await _rolRepository.DeleteAsync(id);
    }




}

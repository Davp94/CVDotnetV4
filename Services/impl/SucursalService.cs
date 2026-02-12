using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class SucursalService(SucursalRepository sucursalRepository) : ISucursalService
{
    private readonly SucursalRepository _sucursalRepository = sucursalRepository;

    public async Task<List<SucursalDto>> FindAllSucursalesAsync()
    {
        var sucursales = await _sucursalRepository.GetAllAsync();
        return [.. sucursales.Select(s => new SucursalDto
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Direccion = s.Direccion,
            Telefono = s.Telefono,
            Ciudad = s.Ciudad
        })];
    }

    public async Task<List<AlmacenDto>> FindAllAlmacenesAsync(int sucursalId)
    {
        var almacenes = await _sucursalRepository.GetAlmacenesBySucursalIdAsync(sucursalId);
        return [.. almacenes.Select(a => new AlmacenDto
        {
            Id = a.Id,
            Nombre = a.Nombre,
            Codigo = a.Codigo,
            Descripcion = a.Descripcion,
            Direccion = a.Direccion,
            Telefono = a.Telefono,
            Ciudad = a.Ciudad
        })];
    }
}
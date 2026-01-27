using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.spec;

public interface IPermisoService
{
    Task<List<PermisoDto>> GetAllAsync();

    Task<PermisoDto?> GetByIdAsync(int id);

    Task<PermisoDto> CreateAsync(CreatePermisoDto dto);

}

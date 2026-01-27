using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;

namespace ComprasVentas.Services.spec;

public interface IRolService
{
    Task<List<RolDto>> GetAllAsync();

    Task<RolDto?> GetByIdAsync(int id);

    Task<RolDto> CreateAsync(CreateRolDto dto);

    Task UpdateAsync(int id, CreateRolDto dto);

    Task DeleteAsync(int id);
}

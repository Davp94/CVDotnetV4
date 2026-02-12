using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.spec;

public interface ISucursalService
{
    Task<List<SucursalDto>> FindAllSucursalesAsync();

    Task<List<AlmacenDto>> FindAllAlmacenesAsync(int sucursalId);
}
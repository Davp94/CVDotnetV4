using System;
using ComprasVentas.Dto;
using ComprasVentas.Dto.common;

namespace ComprasVentas.Services.spec;

public interface IProductoService
{

    Task<PageResultDto<ProductoDto>> GetProductosAsync(ProductoFilterDto productoFilterDto);

    Task<ProductoDto> CreateAsync(CreateProductoDto createProductoDto);

    Task<List<ProductoDto>> FindAllProductosByAlmacenAsync(int almacenId);
}

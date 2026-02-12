using ComprasVentas.Dto;
using ComprasVentas.Dto.common;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet()]
        public async Task<ActionResult<PageResultDto<ProductoDto>>> GetAllProductosPagination([FromQuery] ProductoFilterDto filter)
        {
            var productos = await _productoService.GetProductosAsync(filter);
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CreateProducto([FromForm] CreateProductoDto createProductoDto)
        {
            var producto = await _productoService.CreateAsync(createProductoDto);
            return Ok(producto);
        }

        [HttpGet("almacen/{almacenId}")]
        public async Task<ActionResult<List<ProductoDto>>> FindAllProductosByAlmacen(int almacenId)
        {
            var productos = await _productoService.FindAllProductosByAlmacenAsync(almacenId);
            return Ok(productos);
        }
    }
}

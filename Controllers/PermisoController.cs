using ComprasVentas.Dto;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController(IPermisoService permisoService) : ControllerBase
    {
        private readonly IPermisoService _permisoService = permisoService;

        [HttpGet]
        public async Task<ActionResult<List<PermisoDto>>> GetAll()
        {
            return Ok(await _permisoService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePermisoDto permisoDto)
        {
            var createdPermiso = await _permisoService.CreateAsync(permisoDto);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}

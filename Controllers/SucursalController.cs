using ComprasVentas.Dto;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController(ISucursalService sucursalService) : ControllerBase
    {
        private readonly ISucursalService _sucursalService = sucursalService;

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<List<SucursalDto>>> FindAllSucursales()
        {
            return Ok(await _sucursalService.FindAllSucursalesAsync());
        }

        [HttpGet("/{sucursalId}/almacenes")]
        [Authorize]
        public async Task<ActionResult<List<AlmacenDto>>> FindAllAlmacenes(int sucursalId)
        {
            return Ok(await _sucursalService.FindAllAlmacenesAsync(sucursalId));
        }
    }
}